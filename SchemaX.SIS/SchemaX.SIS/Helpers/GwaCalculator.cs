using System.Globalization;
using SchemaX.SIS.Models;

namespace SchemaX.SIS.Helpers
{
    public static class GwaCalculator
    {
        public const decimal BestGrade = 1.00m;   // grading scale assumed 1.00 - 5.00
        public const decimal WorstGrade = 5.00m;

        /// <returns>The GWA rounded to 2 decimals, or null when nothing is graded yet.</returns>
        public static decimal? Compute(IEnumerable<EnrolledSubject> subjects)
        {
            var graded = subjects.Where(s => s.Grade.HasValue && s.Units > 0).ToList();
            if (graded.Count == 0) return null;               // never 0.00

            decimal totalUnits = graded.Sum(s => (decimal)s.Units);
            decimal totalPoints = graded.Sum(s => s.Grade!.Value * s.Units);

            // Same rounding as MySQL ROUND(): halves go away from zero.
            return Math.Round(totalPoints / totalUnits, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>"1.75" or "N/A": what the details page label should show.</summary>
        public static string Format(decimal? gwa) =>
            gwa.HasValue ? gwa.Value.ToString("0.00", CultureInfo.InvariantCulture) : "N/A";

        /// <summary>True for 1.00 to 5.00 with at most 2 decimals (the column is DECIMAL(3,2)).</summary>
        public static bool IsValidGrade(decimal grade) =>
            grade >= BestGrade && grade <= WorstGrade && decimal.Round(grade, 2) == grade;
    }
}