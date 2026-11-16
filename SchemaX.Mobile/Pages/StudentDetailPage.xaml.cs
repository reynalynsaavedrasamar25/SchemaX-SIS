namespace SchemaX.Mobile.Pages;

[QueryProperty(nameof(StudentId), "studentId")]
public partial class StudentDetailPage : ContentPage
{
    public string StudentId
    {
        set => StudentIdLabel.Text = $"Student ID: {value}";
    }

    public StudentDetailPage()
    {
        InitializeComponent();
    }
}