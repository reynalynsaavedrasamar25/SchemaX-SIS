using MySqlConnector;
using SchemaX.SIS.Models;

namespace SchemaX.SIS.Data
{
    public class AcademicRepository
    {
        // Subjects the student is enrolled in (with grade, null = not graded)
        public List<EnrolledSubject> GetEnrolled(string studentId)
        {
            var list = new List<EnrolledSubject>();
            using var connection = Db.Open();

            string query = @"
                SELECT ss.id, ss.student_id, ss.subject_id,
                       s.subject_code, s.title, s.units, ss.grade
                FROM student_subjects ss
                JOIN subjects s ON s.subject_id = ss.subject_id
                WHERE ss.student_id = @student_id
                ORDER BY s.subject_code;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new EnrolledSubject
                {
                    Id = Convert.ToInt32(reader["id"]),
                    StudentId = Convert.ToString(reader["student_id"]) ?? "",
                    SubjectId = Convert.ToInt32(reader["subject_id"]),
                    SubjectCode = Convert.ToString(reader["subject_code"]) ?? "",
                    Title = Convert.ToString(reader["title"]) ?? "",
                    Units = Convert.ToInt32(reader["units"]),
                    Grade = reader["grade"] == DBNull.Value ? null : Convert.ToDecimal(reader["grade"])
                });
            }
            return list;
        }

        // Subjects the student is NOT enrolled in yet (for the picker)
        public List<Subject> GetAvailableSubjects(string studentId)
        {
            var list = new List<Subject>();
            using var connection = Db.Open();

            string query = @"
                SELECT subject_id, subject_code, title, units
                FROM subjects
                WHERE subject_id NOT IN
                      (SELECT subject_id FROM student_subjects WHERE student_id = @student_id)
                ORDER BY subject_code;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Subject
                {
                    SubjectId = Convert.ToInt32(reader["subject_id"]),
                    SubjectCode = Convert.ToString(reader["subject_code"]) ?? "",
                    Title = Convert.ToString(reader["title"]) ?? "",
                    Units = Convert.ToInt32(reader["units"])
                });
            }
            return list;
        }

        // add ONE subject to a student (grade starts as NULL)
        // Throws MySqlException (DuplicateKeyEntry) if already enrolled.
        public void AddSubject(string studentId, int subjectId)
        {
            using var connection = Db.Open();

            string query = @"
                INSERT INTO student_subjects (student_id, subject_id, grade)
                VALUES (@student_id, @subject_id, NULL);";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);
            command.Parameters.AddWithValue("@subject_id", subjectId);
            command.ExecuteNonQuery();
        }

        // set or change ONE grade (1.00 - 5.00, max 2 decimals)
        public bool SetGrade(int enrollmentId, decimal grade)
        {
            if (grade < 1.00m || grade > 5.00m || decimal.Round(grade, 2) != grade)
                throw new ArgumentOutOfRangeException(nameof(grade),
                    "Grade must be between 1.00 and 5.00 with at most 2 decimals.");

            return UpdateGrade(enrollmentId, grade);
        }

        // clear ONE grade back to NULL
        public bool ClearGrade(int enrollmentId) => UpdateGrade(enrollmentId, null);

        private static bool UpdateGrade(int enrollmentId, decimal? grade)
        {
            using var connection = Db.Open();

            string query = "UPDATE student_subjects SET grade = @grade WHERE id = @id;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@grade", grade.HasValue ? grade.Value : DBNull.Value);
            command.Parameters.AddWithValue("@id", enrollmentId);
            return command.ExecuteNonQuery() > 0;
        }

        // Remove ONE enrolled subject (its grade goes with it). Other rows are untouched.
        public bool RemoveSubject(int enrollmentId)
        {
            using var connection = Db.Open();

            string query = "DELETE FROM student_subjects WHERE id = @id;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", enrollmentId);
            return command.ExecuteNonQuery() > 0;
        }

        public Subject? GetSubjectByCode(string code)
        {
            using var con = Db.Open();
            using var cmd = new MySqlCommand(
                "SELECT subject_id, subject_code, title, units FROM subjects WHERE subject_code = @c;", con);
            cmd.Parameters.AddWithValue("@c", code);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            return new Subject
            {
                SubjectId = Convert.ToInt32(r["subject_id"]),
                SubjectCode = Convert.ToString(r["subject_code"]) ?? "",
                Title = Convert.ToString(r["title"]) ?? "",
                Units = Convert.ToInt32(r["units"])
            };
        }

        // Find or create the subject by code, then add it to the student (grade may be null).
        public void AddSubjectToStudent(string studentId, string code, string title, int units, decimal? grade)
        {
            using var con = Db.Open();
            using var tx = con.BeginTransaction();

            int subjectId = 0;
            using (var find = new MySqlCommand(
                "SELECT subject_id, title, units FROM subjects WHERE subject_code = @c;", con, tx))
            {
                find.Parameters.AddWithValue("@c", code);
                using var r = find.ExecuteReader();
                if (r.Read())
                {
                    string oldTitle = Convert.ToString(r["title"]) ?? "";
                    int oldUnits = Convert.ToInt32(r["units"]);
                    if (!oldTitle.Equals(title, StringComparison.OrdinalIgnoreCase) || oldUnits != units)
                        throw new InvalidOperationException(
                            $"{code} already exists as \"{oldTitle}\" ({oldUnits} units). " +
                            "Use the same subject name and units, or a different subject code.");
                    subjectId = Convert.ToInt32(r["subject_id"]);
                }
            }

            if (subjectId == 0)
            {
                using var ins = new MySqlCommand(
                    "INSERT INTO subjects (subject_code, title, units) VALUES (@c, @t, @u); SELECT LAST_INSERT_ID();",
                    con, tx);
                ins.Parameters.AddWithValue("@c", code);
                ins.Parameters.AddWithValue("@t", title);
                ins.Parameters.AddWithValue("@u", units);
                subjectId = Convert.ToInt32(ins.ExecuteScalar());
            }

            using (var link = new MySqlCommand(
                "INSERT INTO student_subjects (student_id, subject_id, grade) VALUES (@s, @sub, @g);", con, tx))
            {
                link.Parameters.AddWithValue("@s", studentId);
                link.Parameters.AddWithValue("@sub", subjectId);
                link.Parameters.AddWithValue("@g", grade.HasValue ? grade.Value : DBNull.Value);
                link.ExecuteNonQuery();
            }

            tx.Commit();
        }

    }
}