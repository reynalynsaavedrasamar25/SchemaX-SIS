using MySqlConnector;
using SchemaX.Mobile.Models;

namespace SchemaX.Mobile.Services;

public class MySqlStudentService : IStudentService
{
    private const int DemoUserId = 1;
    private readonly DbConfig _config;

    public MySqlStudentService(DbConfig config)
    {
        _config = config;
    }

    private async Task<MySqlConnection> OpenAsync(CancellationToken ct)
    {
        var conn = new MySqlConnection(_config.BuildConnectionString());
        await conn.OpenAsync(ct);
        return conn;
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

    public async Task<IReadOnlyList<Student>> GetStudentsAsync(
        string? search = null,
        string? program = null,
        int? year = null,
        CancellationToken ct = default)
    {
        var list = new List<Student>();

        await using var conn = await OpenAsync(ct);
        var sql = @"SELECT student_id, user_id, first_name, middle_name, surname, gender,
                           year_level, program, bloc, birthday, bu_email, contact_no
                    FROM students
                    WHERE user_id = @uid";

        if (!string.IsNullOrWhiteSpace(search))
        {
            sql += @" AND (student_id LIKE @q OR first_name LIKE @q OR surname LIKE @q
                           OR program LIKE @q OR bu_email LIKE @q)";
        }
        if (!string.IsNullOrWhiteSpace(program))
            sql += " AND program = @program";
        if (year.HasValue)
            sql += " AND year_level = @year";

        sql += " ORDER BY surname, first_name";

        await using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@uid", DemoUserId);
        if (!string.IsNullOrWhiteSpace(search))
            cmd.Parameters.AddWithValue("@q", $"%{search.Trim()}%");
        if (!string.IsNullOrWhiteSpace(program))
            cmd.Parameters.AddWithValue("@program", program.Trim());
        if (year.HasValue)
            cmd.Parameters.AddWithValue("@year", year.Value);

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
            list.Add(Map(reader));

        return list;
    }

    public async Task<Student?> GetByIdAsync(string studentId, CancellationToken ct = default)
    {
        await using var conn = await OpenAsync(ct);
        await using var cmd = new MySqlCommand(
            @"SELECT student_id, user_id, first_name, middle_name, surname, gender,
                     year_level, program, bloc, birthday, bu_email, contact_no
              FROM students
              WHERE student_id = @id AND user_id = @uid", conn);
        cmd.Parameters.AddWithValue("@id", studentId);
        cmd.Parameters.AddWithValue("@uid", DemoUserId);

        await using var reader = await cmd.ExecuteReaderAsync(ct);
        return await reader.ReadAsync(ct) ? Map(reader) : null;
    }

    public async Task AddAsync(Student student, CancellationToken ct = default)
    {
        await using var conn = await OpenAsync(ct);
        await using var cmd = new MySqlCommand(
            @"INSERT INTO students
                (student_id, user_id, first_name, middle_name, surname, gender,
                 year_level, program, bloc, birthday, bu_email, contact_no)
              VALUES
                (@id, @uid, @first, @middle, @surname, @gender,
                 @year, @program, @bloc, @birthday, @email, @contact)", conn);
        Bind(cmd, student);
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async Task UpdateAsync(Student student, CancellationToken ct = default)
    {
        await using var conn = await OpenAsync(ct);
        await using var cmd = new MySqlCommand(
            @"UPDATE students SET
                first_name = @first, middle_name = @middle, surname = @surname,
                gender = @gender, year_level = @year, program = @program,
                bloc = @bloc, birthday = @birthday, bu_email = @email,
                contact_no = @contact
              WHERE student_id = @id AND user_id = @uid", conn);
        Bind(cmd, student);
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async Task DeleteAsync(string studentId, CancellationToken ct = default)
    {
        await using var conn = await OpenAsync(ct);
        await using var cmd = new MySqlCommand(
            "DELETE FROM students WHERE student_id = @id AND user_id = @uid", conn);
        cmd.Parameters.AddWithValue("@id", studentId);
        cmd.Parameters.AddWithValue("@uid", DemoUserId);
        await cmd.ExecuteNonQueryAsync(ct);
    }

    public async Task<bool> PingAsync(CancellationToken ct = default)
    {
        try
        {
            await using var conn = await OpenAsync(ct);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static void Bind(MySqlCommand cmd, Student s)
    {
        cmd.Parameters.AddWithValue("@id", s.StudentId);
        cmd.Parameters.AddWithValue("@uid", DemoUserId);
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