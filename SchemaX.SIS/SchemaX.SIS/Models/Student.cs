
using System;

namespace SchemaX.SIS.Models
{
    public class Student
    {
        public string StudentId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Surname { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;
        public int YearLevel { get; set; }
        public string Program { get; set; } = string.Empty;
        public string Bloc { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }

        public string BuEmail { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string? PhotoPath { get; set; }
        public string? PhotoPath { get; set; }

        public string FullName
        {
            get
            {
                string first = FirstName.Trim();
                string middle = MiddleName?.Trim() ?? "";
                string last = Surname.Trim();

                if (middle.Length == 0)
                {
                    return $"{first} {last}".Trim();
                }

                return $"{first} {char.ToUpper(middle[0])}. {last}".Trim();
            }
        }
    }
}
