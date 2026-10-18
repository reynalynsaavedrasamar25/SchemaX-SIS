using SchemaX.SIS.Helpers;
using SchemaX.SIS.Models;
using Xunit;

namespace SchemaX.ValidatorTests
{
    public class GwaCalculatorTests
    {
        private static EnrolledSubject Sub(int units, decimal? grade) =>
            new EnrolledSubject { Units = units, Grade = grade };

        // WITH grades
        [Fact]
        public void WithGrades_GivesWeightedAverage()
        {
            // (1.00*3 + 2.00*1) / 4 = 1.25
            var list = new[] { Sub(3, 1.00m), Sub(1, 2.00m) };
            Assert.Equal(1.25m, GwaCalculator.Compute(list));
        }

        // WITHOUT grades
        [Fact]
        public void WithoutGrades_ShowsNA()
        {
            var list = new[] { Sub(3, null), Sub(3, null) };
            Assert.Null(GwaCalculator.Compute(list));
            Assert.Equal("N/A", GwaCalculator.Format(GwaCalculator.Compute(list)));
        }

        // MIXED (some graded, some not): Reynalyn's real data
        [Fact]
        public void Mixed_IgnoresSubjectWithoutGrade()
        {
            var list = new[] { Sub(3, 2.00m), Sub(3, 1.50m), Sub(3, null) };
            Assert.Equal(1.75m, GwaCalculator.Compute(list));
        }
    }
}