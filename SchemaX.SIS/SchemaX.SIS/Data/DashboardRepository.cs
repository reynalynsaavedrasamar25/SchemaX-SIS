using MySqlConnector;
using SchemaX.SIS.Helpers;

namespace SchemaX.SIS.Data
{
    public record DashboardCounts(int Students, int Subjects, int Enrollments, int UngradedEnrollments);

    public class DashboardRepository
    {
        public DashboardCounts GetCounts()
        {
            using var connection = Db.Open();
            int uid = Session.CurrentUserId;

            int students = CountRows(connection,
                "SELECT COUNT(*) FROM students WHERE user_id = @uid", uid);

            int subjects = CountRows(connection,
                "SELECT COUNT(DISTINCT ss.subject_id) FROM student_subjects ss " +
                "INNER JOIN students st ON ss.student_id = st.student_id " +
                "WHERE st.user_id = @uid", uid);

            int enrollments = CountRows(connection,
                "SELECT COUNT(*) FROM student_subjects ss " +
                "INNER JOIN students st ON ss.student_id = st.student_id " +
                "WHERE st.user_id = @uid", uid);

            int ungraded = CountRows(connection,
                "SELECT COUNT(*) FROM student_subjects ss " +
                "INNER JOIN students st ON ss.student_id = st.student_id " +
                "WHERE st.user_id = @uid AND ss.grade IS NULL", uid);

            return new DashboardCounts(students, subjects, enrollments, ungraded);
        }

        private static int CountRows(MySqlConnection connection, string sql, int uid)
        {
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@uid", uid);
            return Convert.ToInt32(command.ExecuteScalar());
        }


        public Dictionary<int, int> GetStudentsPerYear()
        {
            var result = new Dictionary<int, int>();
            using var connection = Db.Open();

            string query = @"
                SELECT year_level, COUNT(*) AS total 
                FROM students 
                WHERE user_id = @uid 
                GROUP BY year_level 
                ORDER BY year_level;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@uid", Session.CurrentUserId);
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
                WHERE user_id = @uid
                GROUP BY program
                ORDER BY program;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@uid", Session.CurrentUserId);
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
                JOIN students st ON st.student_id = ss.student_id
                WHERE ss.student_id = @student_id
                  AND st.user_id = @uid
                  AND ss.grade IS NOT NULL
                  AND s.units > 0;";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@student_id", studentId);
            command.Parameters.AddWithValue("@uid", Session.CurrentUserId);

            object? value = command.ExecuteScalar();
            return value == null || value == DBNull.Value ? null : Convert.ToDecimal(value);
        }
    }
}
