using MySqlConnector;

namespace SchemaX.SIS.Data
{
    public record DashboardCounts(int Students, int Subjects, int Enrollments, int UngradedEnrollments);

    public class DashboardRepository
    {
        public DashboardCounts GetCounts()
        {
            using var connection = Db.Open();

            string query = @"
                SELECT (SELECT COUNT(*) FROM students)                             AS students,
                       (SELECT COUNT(*) FROM subjects)                             AS subjects,
                       (SELECT COUNT(*) FROM student_subjects)                     AS enrollments,
                       (SELECT COUNT(*) FROM student_subjects WHERE grade IS NULL) AS ungraded;";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            reader.Read();

            return new DashboardCounts(
                Convert.ToInt32(reader["students"]),
                Convert.ToInt32(reader["subjects"]),
                Convert.ToInt32(reader["enrollments"]),
                Convert.ToInt32(reader["ungraded"]));
        }

        public Dictionary<int, int> GetStudentsPerYear()
        {
            var result = new Dictionary<int, int>();
            using var connection = Db.Open();

            string query = @"
                SELECT year_level, COUNT(*) AS total
                FROM students
                GROUP BY year_level
                ORDER BY year_level;";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                result[Convert.ToInt32(reader["year_level"])] = Convert.ToInt32(reader["total"]);
            return result;
        }

        public Dictionary<string, int> GetStudentsPerProgram()
        {
            var result = new Dictionary<string, int>();
            using var connection = Db.Open();

            string query = @"
                SELECT program, COUNT(*) AS total
                FROM students
                GROUP BY program
                ORDER BY program;";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                result[Convert.ToString(reader["program"]) ?? ""] = Convert.ToInt32(reader["total"]);
            return result;
        }

        // GWA = SUM(grade * units) / SUM(units), graded subjects only.
        // Returns null when there are no grades (shown as "N/A", never 0.00).
        public decimal? GetGwa(string studentId)
        {
            using var connection = Db.Open();

            string query = @"
                SELECT ROUND(SUM(ss.grade * s.units) / SUM(s.units), 2)
                FROM student_subjects ss
                JOIN subjects s ON s.subject_id = ss.subject_id
                WHERE ss.student_id = @student_id
                  AND ss.grade IS NOT NULL
                  AND s.units > 0;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);

            object? value = command.ExecuteScalar();
            return value == null || value == DBNull.Value ? null : Convert.ToDecimal(value);
        }
    }
}
