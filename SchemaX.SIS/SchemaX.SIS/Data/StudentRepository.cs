using MySqlConnector;
using SchemaX.SIS.Models;

namespace SchemaX.SIS.Data
{
    public partial class StudentRepository
    {
        public void Insert(Student student)
        {
            using var connection = Db.Open();

            string query = @"
                INSERT INTO students
                (student_id, first_name, middle_name, surname, gender,
                 year_level, program, bloc, birthday, bu_email, contact_no, photo_path)
                VALUES
                (@student_id, @first_name, @middle_name, @surname, @gender,
                 @year_level, @program, @bloc, @birthday, @bu_email, @contact_no, @photo_path);";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@student_id", student.StudentId);
            command.Parameters.AddWithValue("@first_name", student.FirstName);
            command.Parameters.AddWithValue("@middle_name", (object?)student.MiddleName ?? DBNull.Value);
            command.Parameters.AddWithValue("@surname", student.Surname);
            command.Parameters.AddWithValue("@gender", student.Gender);
            command.Parameters.AddWithValue("@year_level", student.YearLevel);
            command.Parameters.AddWithValue("@program", student.Program);
            command.Parameters.AddWithValue("@bloc", student.Bloc);
            command.Parameters.AddWithValue("@birthday", student.Birthday.Date);
            command.Parameters.AddWithValue("@bu_email", student.BuEmail);
            command.Parameters.AddWithValue("@contact_no", student.ContactNo);
            command.Parameters.AddWithValue("@photo_path", (object?)student.PhotoPath ?? DBNull.Value);

            command.ExecuteNonQuery();
        }
    }
}