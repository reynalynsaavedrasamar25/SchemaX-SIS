using SchemaX.SIS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.Mobile.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync(string? search = null, string? program = null, int? yearLevel = null);
        Task<Student?> GetByIdAsync(string studentId);
        Task<bool> LoginAsync(string username, string password);
    }
}
