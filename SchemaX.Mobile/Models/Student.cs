namespace SchemaX.Mobile.Models;

public class Student
{
    public string StudentId { get; set; } = string.Empty;
    public int UserId { get; set; }
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

    public string FullName => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {Surname}"
        : $"{FirstName} {MiddleName} {Surname}";

    public string Initials
    {
        get
        {
            char first = !string.IsNullOrWhiteSpace(FirstName) ? char.ToUpperInvariant(FirstName[0]) : '?';
            char last = !string.IsNullOrWhiteSpace(Surname) ? char.ToUpperInvariant(Surname[0]) : '?';
            return $"{first}{last}";
        }
    }
}