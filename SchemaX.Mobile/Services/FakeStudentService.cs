using SchemaX.Mobile.Models;

namespace SchemaX.Mobile.Services;

public class FakeStudentService : IStudentService
{
    private readonly List<Student> _students =
    [
        new() { StudentId = "2025-01-0001", UserId = 1, FirstName = "Juan", MiddleName = "Reyes", Surname = "Dela Cruz", Gender = "Male", YearLevel = 2, Program = "BSCS", Bloc = "A", Birthday = new DateTime(2005, 4, 12), BuEmail = "juan.delacruz@bicol-u.edu.ph", ContactNo = "09171234501" },
        new() { StudentId = "2025-01-0002", UserId = 1, FirstName = "Maria", MiddleName = "Santos", Surname = "Clara", Gender = "Female", YearLevel = 2, Program = "BSCS", Bloc = "A", Birthday = new DateTime(2005, 8, 21), BuEmail = "maria.clara@bicol-u.edu.ph", ContactNo = "09171234502" },
        new() { StudentId = "2025-01-0003", UserId = 1, FirstName = "Rafael", MiddleName = null, Surname = "Mendoza", Gender = "Male", YearLevel = 1, Program = "BSIT", Bloc = "B", Birthday = new DateTime(2006, 1, 15), BuEmail = "rafael.mendoza@bicol-u.edu.ph", ContactNo = "09171234503" },
        new() { StudentId = "2025-01-0004", UserId = 1, FirstName = "Camille", MiddleName = "Diaz", Surname = "Navarro", Gender = "Female", YearLevel = 3, Program = "BSCS", Bloc = "B", Birthday = new DateTime(2004, 11, 3), BuEmail = "camille.navarro@bicol-u.edu.ph", ContactNo = "09171234504" },
        new() { StudentId = "2025-01-0005", UserId = 1, FirstName = "Gabriel", MiddleName = "Cruz", Surname = "Bautista", Gender = "Male", YearLevel = 4, Program = "BSIT", Bloc = "A", Birthday = new DateTime(2003, 6, 30), BuEmail = "gabriel.bautista@bicol-u.edu.ph", ContactNo = "09171234505" },
        new() { StudentId = "2025-01-0006", UserId = 1, FirstName = "Sofia", MiddleName = "Gomez", Surname = "Villanueva", Gender = "Female", YearLevel = 1, Program = "BSCS", Bloc = "A", Birthday = new DateTime(2006, 9, 18), BuEmail = "sofia.villanueva@bicol-u.edu.ph", ContactNo = "09171234506" },
        new() { StudentId = "2025-01-0007", UserId = 1, FirstName = "Miguel", MiddleName = "Torres", Surname = "Aquino", Gender = "Male", YearLevel = 3, Program = "BSIT", Bloc = "C", Birthday = new DateTime(2004, 2, 25), BuEmail = "miguel.aquino@bicol-u.edu.ph", ContactNo = "09171234507" },
        new() { StudentId = "2025-01-0008", UserId = 1, FirstName = "Isabella", MiddleName = null, Surname = "Ramos", Gender = "Female", YearLevel = 4, Program = "BSCS", Bloc = "A", Birthday = new DateTime(2003, 12, 8), BuEmail = "isabella.ramos@bicol-u.edu.ph", ContactNo = "09171234508" },
        new() { StudentId = "2025-01-0009", UserId = 1, FirstName = "Daniel", MiddleName = "Lopez", Surname = "Castillo", Gender = "Male", YearLevel = 2, Program = "BSIT", Bloc = "B", Birthday = new DateTime(2005, 5, 19), BuEmail = "daniel.castillo@bicol-u.edu.ph", ContactNo = "09171234509" },
        new() { StudentId = "2025-01-0010", UserId = 1, FirstName = "Bianca", MiddleName = "Flores", Surname = "Morales", Gender = "Female", YearLevel = 1, Program = "BSIT", Bloc = "A", Birthday = new DateTime(2006, 7, 14), BuEmail = "bianca.morales@bicol-u.edu.ph", ContactNo = "09171234510" },
        new() { StudentId = "2025-01-0011", UserId = 1, FirstName = "Lorenzo", MiddleName = "Perez", Surname = "Santiago", Gender = "Male", YearLevel = 3, Program = "BSCS", Bloc = "A", Birthday = new DateTime(2004, 3, 27), BuEmail = "lorenzo.santiago@bicol-u.edu.ph", ContactNo = "09171234511" },
        new() { StudentId = "2025-01-0012", UserId = 1, FirstName = "Angela", MiddleName = null, Surname = "Domingo", Gender = "Female", YearLevel = 2, Program = "BSCS", Bloc = "B", Birthday = new DateTime(2005, 10, 9), BuEmail = "angela.domingo@bicol-u.edu.ph", ContactNo = "09171234512" },
        new() { StudentId = "2025-01-0013", UserId = 1, FirstName = "Enzo", MiddleName = "Rivera", Surname = "Mercado", Gender = "Male", YearLevel = 4, Program = "BSIT", Bloc = "B", Birthday = new DateTime(2003, 1, 22), BuEmail = "enzo.mercado@bicol-u.edu.ph", ContactNo = "09171234513" },
        new() { StudentId = "2025-01-0014", UserId = 1, FirstName = "Katrina", MiddleName = "Vargas", Surname = "Salazar", Gender = "Female", YearLevel = 3, Program = "BSIT", Bloc = "A", Birthday = new DateTime(2004, 8, 5), BuEmail = "katrina.salazar@bicol-u.edu.ph", ContactNo = "09171234514" },
        new() { StudentId = "2025-01-0015", UserId = 1, FirstName = "Mateo", MiddleName = "Castro", Surname = "Ortega", Gender = "Male", YearLevel = 1, Program = "BSCS", Bloc = "B", Birthday = new DateTime(2006, 11, 29), BuEmail = "mateo.ortega@bicol-u.edu.ph", ContactNo = "09171234515" }
    ];

    public Task<IReadOnlyList<Student>> GetStudentsAsync(
        string? search = null,
        string? program = null,
        int? year = null,
        CancellationToken ct = default)
    {
        IEnumerable<Student> query = _students;

        if (!string.IsNullOrWhiteSpace(search))
        {
            string q = search.Trim();
            query = query.Where(s =>
                s.StudentId.Contains(q, StringComparison.OrdinalIgnoreCase) ||
                s.FullName.Contains(q, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(program) && !program.Equals("All", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(s => s.Program.Equals(program, StringComparison.OrdinalIgnoreCase));
        }

        if (year.HasValue && year.Value > 0)
        {
            query = query.Where(s => s.YearLevel == year.Value);
        }

        IReadOnlyList<Student> result = query
            .OrderBy(s => s.Surname)
            .ThenBy(s => s.FirstName)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Student?> GetByIdAsync(string studentId, CancellationToken ct = default) =>
        Task.FromResult(_students.FirstOrDefault(s => s.StudentId == studentId));

    public Task AddAsync(Student student, CancellationToken ct = default)
    {
        _students.Add(student);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Student student, CancellationToken ct = default)
    {
        int idx = _students.FindIndex(s => s.StudentId == student.StudentId);
        if (idx >= 0) _students[idx] = student;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string studentId, CancellationToken ct = default)
    {
        _students.RemoveAll(s => s.StudentId == studentId);
        return Task.CompletedTask;
    }

    public Task<bool> PingAsync(CancellationToken ct = default) => Task.FromResult(true);
}