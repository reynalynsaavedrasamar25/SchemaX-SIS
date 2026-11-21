using SchemaX.SIS.Models;

namespace SchemaX.Mobile.Services;

public interface IStudentService
{
    Task<IReadOnlyList<Student>> GetStudentsAsync(
        string? search = null,
        string? program = null,
        int? year = null,
        CancellationToken ct = default);

    Task<Student?> GetByIdAsync(string studentId, CancellationToken ct = default);
    Task AddAsync(Student student, CancellationToken ct = default);
    Task UpdateAsync(Student student, CancellationToken ct = default);
    Task DeleteAsync(string studentId, CancellationToken ct = default);
    Task<bool> PingAsync(CancellationToken ct = default);
    Task<bool> LoginAsync(string username, string password, CancellationToken ct = default);
}