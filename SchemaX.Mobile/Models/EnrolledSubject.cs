namespace SchemaX.SIS.Models
{
    public class EnrolledSubject
    {
        public int Id { get; set; }              // student_subjects.id
        public string StudentId { get; set; } = "";
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; } = "";
        public string Title { get; set; } = "";
        public int Units { get; set; }
        public decimal? Grade { get; set; }      // null = not graded yet
    }
}