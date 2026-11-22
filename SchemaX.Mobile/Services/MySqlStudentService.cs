using System.Security.Cryptography;
using System.Text;
using MySqlConnector;
using SchemaX.SIS.Models;

namespace SchemaX.Mobile.Services;

public class MySqlStudentService : IStudentService
{
    private const string Columns =
        "student_id, user_id, first_name, middle_name, surname, gender, " +
        "year_level, program, bloc, birthday, bu_email, contact_no";

    private readonly DbConfig _config;
    private readonly UserSession _session;

    public MySqlStudentService(DbConfig config, UserSession session)
    {
        _config = config;
        _session = session;
    }

    // Opens a connection, runs the work, and turns database failures into friendly errors.
    private async Task<T> RunAsync<T>(Func<MySqlConnection, Task<T>> work, CancellationToken ct)
    {
        try
        {
            await using var conn = new MySqlConnection(_config.BuildConnectionString());
            await conn.OpenAsync(ct);
            return await work(conn);
        }
        catch (OperationCanceledException)
        {
            throw; // normal during search-as-you-type, not an error
        }
        catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
        {
            throw new DataAccessException("Student ID or email already exists.", ex);
        }
        catch (MySqlException ex) when (ex.ErrorCode == MySqlErrorCode.NoReferencedRow2
                                     || ex.ErrorCode == MySqlErrorCode.RowIsReferenced2)
        {
            throw new DataAccessException("This record is linked to other data and cannot be changed.", ex);
        }
        catch (Exception ex) when (ex is MySqlException or TimeoutException)
        {
            System.Diagnostics.Debug.WriteLine($"[DB] {ex.GetType().Name}: {ex.Message}");
            throw new DataAccessException("Cannot reach the database. Check that MySQL is running.", ex);
        }
    }

    private static Student Map(MySqlDataReader r) => new()
    {
        StudentId = r.GetString("student_id"),
        UserId = r.GetInt32("user_id"),
        FirstName = r.GetString("first_name"),
        MiddleName = r.IsDBNull(r.GetOrdinal("middle_name")) ? null : r.GetString("middle_name"),
        Surname = r.GetString("surname"),
        Gender = r.GetString("gender"),
        YearLevel = r.GetInt32("year_level"),
        Program = r.GetString("program"),
        Bloc = r.GetString("bloc"),
        Birthday = r.GetDateTime("birthday"),
        BuEmail = r.GetString("bu_email"),
        ContactNo = r.GetString("contact_no")
    };

    public Task<IReadOnlyList<Student>> GetStudentsAsync(
        string? search = null, string? program = null, int? year = null, CancellationToken ct = default)
        => RunAsync<IReadOnlyList<Student>>(async conn =>
        {
            var sql = $"SELECT {Columns} FROM students WHERE user_id = @uid";

            if (!string.IsNullOrWhiteSpace(search))
                sql += " AND (student_id LIKE @q OR CONCAT_WS(' ', first_name, middle_name, surname) LIKE @q)";
            if (!string.IsNullOrWhiteSpace(program))
                sql += " AND program = @program";
            if (year.HasValue)
                sql += " AND year_level = @year";

            sql += " ORDER BY surname, first_name";

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", _session.UserId);
            if (!string.IsNullOrWhiteSpace(search))
                cmd.Parameters.AddWithValue("@q", $"%{search.Trim()}%");
            if (!string.IsNullOrWhiteSpace(program))
                cmd.Parameters.AddWithValue("@program", program.Trim());
            if (year.HasValue)
                cmd.Parameters.AddWithValue("@year", year.Value);

            var list = new List<Student>();
            await using var reader = await cmd.ExecuteReaderAsync(ct);
            while (await reader.ReadAsync(ct))
                list.Add(Map(reader));
            return list;
        }, ct);

    public Task<Student?> GetByIdAsync(string studentId, CancellationToken ct = default)
        => RunAsync<Student?>(async conn =>
        {
            await using var cmd = new MySqlCommand(
                $"SELECT {Columns} FROM students WHERE student_id = @id AND user_id = @uid", conn);
            cmd.Parameters.AddWithValue("@id", studentId);
            cmd.Parameters.AddWithValue("@uid", _session.UserId);

            await using var reader = await cmd.ExecuteReaderAsync(ct);
            return await reader.ReadAsync(ct) ? Map(reader) : null;
        }, ct);

    public async Task AddAsync(Student student, CancellationToken ct = default)
        => await RunAsync(async conn =>
        {
            await using var cmd = new MySqlCommand(
                @"INSERT INTO students
                    (student_id, user_id, first_name, middle_name, surname, gender,
                     year_level, program, bloc, birthday, bu_email, contact_no)
                  VALUES
                    (@id, @uid, @first, @middle, @surname, @gender,
                     @year, @program, @bloc, @birthday, @email, @contact)", conn);
            Bind(cmd, student);
            await cmd.ExecuteNonQueryAsync(ct);
            return true;
        }, ct);

    public async Task UpdateAsync(Student student, CancellationToken ct = default)
        => await RunAsync(async conn =>
        {
            await using var cmd = new MySqlCommand(
                @"UPDATE students SET
                    first_name = @first, middle_name = @middle, surname = @surname,
                    gender = @gender, year_level = @year, program = @program,
                    bloc = @bloc, birthday = @birthday, bu_email = @email,
                    contact_no = @contact
                  WHERE student_id = @id AND user_id = @uid", conn);
            Bind(cmd, student);
            await cmd.ExecuteNonQueryAsync(ct);
            return true;
        }, ct);

    public async Task DeleteAsync(string studentId, CancellationToken ct = default)
        => await RunAsync(async conn =>
        {
            await using var cmd = new MySqlCommand(
                "DELETE FROM students WHERE student_id = @id AND user_id = @uid", conn);
            cmd.Parameters.AddWithValue("@id", studentId);
            cmd.Parameters.AddWithValue("@uid", _session.UserId);
            await cmd.ExecuteNonQueryAsync(ct);
            return true;
        }, ct);

    public async Task<bool> PingAsync(CancellationToken ct = default)
    {
        try
        {
            return await RunAsync(conn => Task.FromResult(true), ct);
        }
        catch (DataAccessException)
        {
            return false;
        }
    }

    public Task<bool> LoginAsync(string username, string password, CancellationToken ct = default)
    => RunAsync(async conn =>
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            return false;

        await using var cmd = new MySqlCommand(
            "SELECT user_id, password_hash FROM users WHERE username = @un", conn);
        cmd.Parameters.AddWithValue("@un", username.Trim());

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        if (!await reader.ReadAsync(ct)) return false;                 // no such username
        if (!VerifyPassword(password, reader.GetString("password_hash")))
            return false;                                              // wrong password

        _session.UserId = reader.GetInt32("user_id");                  // every later query uses this id
        return true;
    }, ct);

    // Must stay identical to the desktop PasswordHasher (SHA-256, hex text).
    private static string HashPassword(string password) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));

    private static bool VerifyPassword(string password, string hash) =>
        string.Equals(HashPassword(password), hash, StringComparison.OrdinalIgnoreCase);

    private void Bind(MySqlCommand cmd, Student s)
    {
        cmd.Parameters.AddWithValue("@id", s.StudentId);
        cmd.Parameters.AddWithValue("@uid", _session.UserId);
        cmd.Parameters.AddWithValue("@first", s.FirstName);
        cmd.Parameters.AddWithValue("@middle", string.IsNullOrWhiteSpace(s.MiddleName) ? DBNull.Value : s.MiddleName);
        cmd.Parameters.AddWithValue("@surname", s.Surname);
        cmd.Parameters.AddWithValue("@gender", s.Gender);
        cmd.Parameters.AddWithValue("@year", s.YearLevel);
        cmd.Parameters.AddWithValue("@program", s.Program);
        cmd.Parameters.AddWithValue("@bloc", s.Bloc);
        cmd.Parameters.AddWithValue("@birthday", s.Birthday.Date);
        cmd.Parameters.AddWithValue("@email", s.BuEmail);
        cmd.Parameters.AddWithValue("@contact", s.ContactNo);
    }
}