using SchemaX.Mobile.Models;

namespace SchemaX.Mobile.Services;

public interface IStudentService
{
    /// <summary>Returns all students, optionally filtered by a search text.</summary>
    Task<List<Student>> GetStudentsAsync(string? search = null);

    /// <summary>Returns one student by ID, or null if not found.</summary>
    Task<Student?> GetByIdAsync(string studentId);

    /// <summary>Adds a new student. Returns true if saved.</summary>
    Task<bool> AddAsync(Student student);

    /// <summary>Updates an existing student. Returns true if saved.</summary>
    Task<bool> UpdateAsync(Student student);

    /// <summary>Deletes a student by ID. Returns true if a row was removed.</summary>
    Task<bool> DeleteAsync(string studentId);

    /// <summary>Checks if the database can be reached (used for the offline banner).</summary>
    Task<bool> PingAsync();
}