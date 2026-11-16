using SchemaX.Mobile.Models;
using SchemaX.Mobile.Services;

namespace SchemaX.Mobile.Pages;

[QueryProperty(nameof(StudentId), "studentId")]
public partial class StudentDetailPage : ContentPage
{
    private readonly IStudentService _studentService;
    private string _studentId = string.Empty;
    private Student? _student;

    public string StudentId
    {
        get => _studentId;
        set
        {
            _studentId = Uri.UnescapeDataString(value ?? string.Empty);
        }
    }

    public StudentDetailPage(IStudentService studentService)
    {
        InitializeComponent();
        _studentService = studentService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadStudentAsync();
    }

    private async Task LoadStudentAsync()
    {
        if (string.IsNullOrWhiteSpace(_studentId)) return;

        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            _student = await _studentService.GetByIdAsync(_studentId);

            if (_student is null)
            {
                await DisplayAlert("Not Found", "This student record could not be found.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            InitialsLabel.Text = _student.Initials;
            FullNameLabel.Text = _student.FullName;
            StudentIdLabel.Text = _student.StudentId;
            AcademicPillLabel.Text = $"{_student.Program} {_student.YearLevel}{_student.Bloc}";

            ProgramLabel.Text = _student.Program;
            YearBlocLabel.Text = $"Year {_student.YearLevel} - Bloc {_student.Bloc}";
            GenderLabel.Text = string.IsNullOrWhiteSpace(_student.Gender) ? "N/A" : _student.Gender;
            BirthdayLabel.Text = _student.Birthday == default ? "N/A" : _student.Birthday.ToString("MMMM dd, yyyy");
            EmailLabel.Text = string.IsNullOrWhiteSpace(_student.BuEmail) ? "N/A" : _student.BuEmail;
            ContactLabel.Text = string.IsNullOrWhiteSpace(_student.ContactNo) ? "N/A" : _student.ContactNo;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load student details: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    private async void OnEditClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_studentId)) return;
        await Shell.Current.GoToAsync($"{nameof(StudentFormPage)}?studentId={Uri.EscapeDataString(_studentId)}");
    }

    private async void OnDeleteClicked(object? sender, EventArgs e)
    {
        if (_student is null) return;

        bool confirm = await DisplayAlert(
            "Delete Student",
            $"Remove {_student.FullName} ({_student.StudentId})? This cannot be undone.",
            "Delete",
            "Cancel");

        if (!confirm) return;

        try
        {
            await _studentService.DeleteAsync(_student.StudentId);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not delete student: {ex.Message}", "OK");
        }
    }
}