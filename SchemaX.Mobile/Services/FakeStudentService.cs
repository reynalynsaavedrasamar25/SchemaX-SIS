using SchemaX.SIS.Models;

namespace SchemaX.Mobile.Services;

public class FakeStudentService : IStudentService
{
    private static readonly List<Student> _students = new()
    {
        new Student { StudentId = "2026-01-00001", FirstName = "John", MiddleName = "Jackson", Surname = "Doe",
                      Program = "BSCS", YearLevel = 2, Gender = "Male", Bloc = "2B", Birthday = new DateTime(2000, 1, 1),
                      BuEmail = "johnjackson.doe26@bicol-u.edu.ph", ContactNo = "09123456789" },
        new Student { StudentId = "2026-01-00002", FirstName = "Jane", MiddleName = "Marie", Surname = "Smith",
                      Program = "BSIT", YearLevel = 3, Gender = "Female", Bloc = "3A", Birthday = new DateTime(2001, 5, 15),
                      BuEmail = "janemarie.smith26@bicol-u.edu.ph", ContactNo = "09123456789" },
        new Student { StudentId = "2026-01-00003", FirstName = "Alice", MiddleName = "Elizabeth", Surname = "Johnson",
                      Program = "BSIT", YearLevel = 1, Gender = "Female", Bloc = "1C", Birthday = new DateTime(2003, 9, 8),
                      BuEmail = "alice.elizabeth.johnson26@bicol-u.edu.ph", ContactNo = "09123456789" },
        new Student { StudentId = "2026-01-00004", FirstName = "Bob", MiddleName = "William", Surname = "Brown",
                      Program = "BSCS", YearLevel = 4, Gender = "Male", Bloc = "4B", Birthday = new DateTime(2002, 12, 25),
                      BuEmail = "bob.william.brown26@bicol-u.edu.ph", ContactNo = "09123456789" }
    };

    public async Task<IReadOnlyList<Student>> GetStudentsAsync(
        string? search = null, string? program = null, int? year = null, CancellationToken ct = default)
    {
        await Task.Yield();

        IEnumerable<Student> query = _students;

        if (!string.IsNullOrWhiteSpace(search))
        {
            string q = search.Trim();
            query = query.Where(s =>
                s.StudentId.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                s.FirstName.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                s.Surname.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(program))
            query = query.Where(s => s.Program.Equals(program, StringComparison.OrdinalIgnoreCase));

        if (year.HasValue)
            query = query.Where(s => s.YearLevel == year.Value);

        return query.OrderBy(s => s.Surname).ThenBy(s => s.FirstName).ToList();
    }

    public async Task<Student?> GetByIdAsync(string studentId, CancellationToken ct = default)
    {
        await Task.Delay(150, ct);
        return _students.FirstOrDefault(s => s.StudentId == studentId);
    }

    public async Task AddAsync(Student student, CancellationToken ct = default)
    {
        await Task.Delay(150, ct);
        _students.Add(student);
    }

    public async Task UpdateAsync(Student student, CancellationToken ct = default)
    {
        await Task.Delay(150, ct);
        int i = _students.FindIndex(s => s.StudentId == student.StudentId);
        if (i >= 0) _students[i] = student;
    }

    public async Task DeleteAsync(string studentId, CancellationToken ct = default)
    {
        await Task.Delay(150, ct);
        _students.RemoveAll(s => s.StudentId == studentId);
    }

    public Task<bool> PingAsync(CancellationToken ct = default) => Task.FromResult(true);

    public async Task<bool> LoginAsync(string username, string password, CancellationToken ct = default)
    {
        await Task.Delay(400, ct);
        // Fake rule: any non-empty username and password works.
        return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
    }
}