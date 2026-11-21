using SchemaX.SIS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.Mobile.Services
{
    public class FakeStudentService : IStudentService
    {
        private static readonly List<Student> _students = new List<Student>
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

        public async Task<List<Student>> GetStudentsAsync(string? search = null, string? program = null, int? yearLevel = null)
        {
            await Task.Delay(300); // simulate network latency so you can see the loading spinner

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

            if (yearLevel.HasValue)
                query = query.Where(s => s.YearLevel == yearLevel.Value);

            return query.OrderBy(s => s.Surname).ThenBy(s => s.FirstName).ToList();
        }

        public async Task<Student?> GetByIdAsync(string studentId)
        {
            await Task.Delay(150);
            return _students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            await Task.Delay(400);
            // Fake rule: any non-empty username and password works.
            return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
        }
    }
}
