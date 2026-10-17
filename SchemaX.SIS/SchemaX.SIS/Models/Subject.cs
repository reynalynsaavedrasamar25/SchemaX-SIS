namespace SchemaX.SIS.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; } = "";
        public string Title { get; set; } = "";
        public int Units { get; set; }           // int, not byte

        public override string ToString() => $"{SubjectCode} - {Title} ({Units} units)";
    }
}