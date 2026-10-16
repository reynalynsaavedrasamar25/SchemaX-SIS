using MySqlConnector;
using SchemaX.SIS.Models;
using System.Reflection.Metadata;

namespace SchemaX.SIS.Data
{
    public class StudentRepository
    {
        // 1: ADD a new Student
        public bool Insert(Student student)
        {
            using var connection = Db.Open();

            string query = @"
                INSERT INTO students
                (
                    student_id,
                    first_name,
                    middle_name,
                    surname,
                    gender,
                    year_level,
                    program,
                    bloc,
                    birthday,
                    bu_email,
                    contact_no,
                    photo_path
                )
                VALUES
                (
                    @student_id,
                    @first_name,
                    @middle_name,
                    @surname,
                    @gender,
                    @year_level,
                    @program,
                    @bloc,
                    @birthday,
                    @bu_email,
                    @contact_no,
                    @photo_path
                );";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@student_id", student.StudentId);

            command.Parameters.AddWithValue(
                "@first_name", student.FirstName);

            command.Parameters.AddWithValue(
                "@middle_name",
                (object?)student.MiddleName ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@surname", student.Surname);

            command.Parameters.AddWithValue(
                "@gender", student.Gender);

            command.Parameters.AddWithValue(
                "@year_level", student.YearLevel);

            command.Parameters.AddWithValue(
                "@program", student.Program);

            command.Parameters.AddWithValue(
                "@bloc", student.Bloc);

            command.Parameters.AddWithValue(
                "@birthday", student.Birthday.Date);

            command.Parameters.AddWithValue(
                "@bu_email", student.BuEmail);

            command.Parameters.AddWithValue(
                "@contact_no", student.ContactNo);

            command.Parameters.AddWithValue(
                "@photo_path",
                (object?)student.PhotoPath ?? DBNull.Value);

            return command.ExecuteNonQuery() > 0;
        }

        // 2. GET a student by ID (for editing)
        public Student? GetById(string studentId)
        {
            using var connection = Db.Open();

            string query = @"
                SELECT *
                FROM students
                WHERE student_id = @student_id;";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@student_id", studentId);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                return null;
            }

            return new Student
            {
                StudentId = Convert.ToString(
                    reader["student_id"]) ?? "",

                FirstName = Convert.ToString(
                    reader["first_name"]) ?? "",

                MiddleName = reader["middle_name"] == DBNull.Value
                    ? null
                    : Convert.ToString(reader["middle_name"]),

                Surname = Convert.ToString(
                    reader["surname"]) ?? "",

                Gender = Convert.ToString(
                    reader["gender"]) ?? "",

                YearLevel = Convert.ToInt32(
                    reader["year_level"]),

                Program = Convert.ToString(
                    reader["program"]) ?? "",

                Bloc = Convert.ToString(
                    reader["bloc"]) ?? "",

                Birthday = Convert.ToDateTime(
                    reader["birthday"]),

                BuEmail = Convert.ToString(
                    reader["bu_email"]) ?? "",

                ContactNo = Convert.ToString(
                    reader["contact_no"]) ?? "",

                PhotoPath = reader["photo_path"] == DBNull.Value
                    ? null
                    : Convert.ToString(reader["photo_path"])
            };
        }

        // 3. UPDATE an existing student
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
                    contact_no = @contact_no,
                    photo_path = @photo_path
                WHERE student_id = @student_id;";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@student_id", student.StudentId);

            command.Parameters.AddWithValue(
                "@first_name", student.FirstName);

            command.Parameters.AddWithValue(
                "@middle_name",
                (object?)student.MiddleName ?? DBNull.Value);

            command.Parameters.AddWithValue(
                "@surname", student.Surname);

            command.Parameters.AddWithValue(
                "@gender", student.Gender);

            command.Parameters.AddWithValue(
                "@year_level", student.YearLevel);

            command.Parameters.AddWithValue(
                "@program", student.Program);

            command.Parameters.AddWithValue(
                "@bloc", student.Bloc);

            command.Parameters.AddWithValue(
                "@birthday", student.Birthday.Date);

            command.Parameters.AddWithValue(
                "@bu_email", student.BuEmail);

            command.Parameters.AddWithValue(
                "@contact_no", student.ContactNo);

            command.Parameters.AddWithValue(
                "@photo_path",
                (object?)student.PhotoPath ?? DBNull.Value);

            return command.ExecuteNonQuery() > 0;
        }

        // 4. DELETE a student by ID
        public bool Delete(string studentId)
        {
            using var connection = Db.Open();

            string query = @"
                DELETE FROM students
                WHERE student_id = @student_id;";

            using var command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@student_id", studentId);

            return command.ExecuteNonQuery() > 0;
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using var connection = Db.Open();

            string query = @"
        SELECT student_id, first_name, middle_name, surname,
               gender, year_level, program, bloc, birthday,
               bu_email, contact_no, photo_path
        FROM students
        ORDER BY student_id;";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student
                {
                    StudentId = reader.GetString("student_id"),
                    FirstName = reader.GetString("first_name"),
                    MiddleName = reader.IsDBNull(
                        reader.GetOrdinal("middle_name"))
                        ? null
                        : reader.GetString("middle_name"),
                    Surname = reader.GetString("surname"),
                    Gender = reader.GetString("gender"),
                    YearLevel = reader.GetInt32("year_level"),
                    Program = reader.GetString("program"),
                    Bloc = reader.GetString("bloc"),
                    Birthday = reader.GetDateTime("birthday"),
                    BuEmail = reader.GetString("bu_email"),
                    ContactNo = reader.GetString("contact_no"),
                    PhotoPath = reader.IsDBNull(
                        reader.GetOrdinal("photo_path"))
                        ? null
                        : reader.GetString("photo_path")
                };

                students.Add(student);
            }

            return students;
        }
    }
}
