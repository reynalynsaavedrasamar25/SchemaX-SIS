using MySqlConnector;
using SchemaX.SIS.Models;
using System.Text;

namespace SchemaX.SIS.Data
{
    public class StudentRepository
    {
        private const string StudentColumns = @"student_id, first_name, middle_name, surname,
            gender, year_level, program, bloc, birthday, bu_email, contact_no, photo_path";

        // SEARCH: null means "no filter"
        public List<Student> Search(string? text, int? yearLevel, string? program)
        {
            var sql = new StringBuilder($"SELECT {StudentColumns} FROM students WHERE 1 = 1");

            using var con = Db.Open();
            using var cmd = new MySqlCommand { Connection = con };

            if (!string.IsNullOrWhiteSpace(text))
            {
                sql.Append(@" AND (student_id LIKE @q
                               OR CONCAT_WS(' ', first_name, middle_name, surname) LIKE @q)");
                cmd.Parameters.AddWithValue("@q", "%" + text.Trim() + "%");
            }
            if (yearLevel.HasValue)
            {
                sql.Append(" AND year_level = @yl");
                cmd.Parameters.AddWithValue("@yl", yearLevel.Value);
            }
            if (!string.IsNullOrWhiteSpace(program))
            {
                sql.Append(" AND program = @pg");
                cmd.Parameters.AddWithValue("@pg", program);
            }
            sql.Append(" ORDER BY surname, first_name");

            cmd.CommandText = sql.ToString();
            using var reader = cmd.ExecuteReader();
            var list = new List<Student>();
            while (reader.Read()) list.Add(MapStudent(reader));
            return list;
        }

        public List<Student> GetAll() => Search(null, null, null);

        // 1: ADD a new Student
        public bool Insert(Student student)
        {
            using var connection = Db.Open();

            string query = @"
                INSERT INTO students
                (student_id, first_name, middle_name, surname, gender, year_level,
                 program, bloc, birthday, bu_email, contact_no, photo_path)
                VALUES
                (@student_id, @first_name, @middle_name, @surname, @gender, @year_level,
                 @program, @bloc, @birthday, @bu_email, @contact_no, @photo_path);";

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

            return command.ExecuteNonQuery() > 0;
        }

        // 2: GET a student by ID (for editing)
        public Student? GetById(string studentId)
        {
            using var connection = Db.Open();

            string query = $"SELECT {StudentColumns} FROM students WHERE student_id = @student_id;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);

            using var reader = command.ExecuteReader();
            return reader.Read() ? MapStudent(reader) : null;
        }

        // 3: UPDATE an existing student (photo_path is intentionally not touched)
        public bool Update(Student student)
        {
            using var connection = Db.Open();

            string query = @"
                UPDATE students
                SET
                    first_name = @first_name,
                    middle_name = @middle_name,
                    surname = @surname,
                    gender = @gender,
                    year_level = @year_level,
                    program = @program,
                    bloc = @bloc,
                    birthday = @birthday,
                    bu_email = @bu_email,
                    contact_no = @contact_no
                WHERE student_id = @student_id;";

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

            return command.ExecuteNonQuery() > 0;
        }

        // 4: DELETE a student by ID
        public bool Delete(string studentId)
        {
            using var connection = Db.Open();

            string query = "DELETE FROM students WHERE student_id = @student_id;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);

            return command.ExecuteNonQuery() > 0;
        }

        private static Student MapStudent(MySqlDataReader r) => new()
        {
            StudentId  = r.GetString("student_id"),
            FirstName  = r.GetString("first_name"),
            MiddleName = r.IsDBNull(r.GetOrdinal("middle_name")) ? null : r.GetString("middle_name"),
            Surname    = r.GetString("surname"),
            Gender     = r.GetString("gender"),
            YearLevel  = r.GetInt32("year_level"),
            Program    = r.GetString("program"),
            Bloc       = r.GetString("bloc"),
            Birthday   = r.GetDateTime("birthday"),
            BuEmail    = r.GetString("bu_email"),
            ContactNo  = r.GetString("contact_no"),
            PhotoPath  = r.IsDBNull(r.GetOrdinal("photo_path")) ? null : r.GetString("photo_path")
        };
    }
}
