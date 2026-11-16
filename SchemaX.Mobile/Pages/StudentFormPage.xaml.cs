using SchemaX.Mobile.Models;
using SchemaX.Mobile.Services;

namespace SchemaX.Mobile.Pages;

[QueryProperty(nameof(StudentId), "studentId")]
public partial class StudentFormPage : ContentPage
{
    private readonly IStudentService _studentService;
    private string _studentId = string.Empty;
    private bool _isEditMode;
    private int _existingUserId = 1;

    public string StudentId
    {
        get => _studentId;
        set
        {
            _studentId = Uri.UnescapeDataString(value ?? string.Empty);
            _isEditMode = !string.IsNullOrWhiteSpace(_studentId);
        }
    }

    public StudentFormPage(IStudentService studentService)
    {
        InitializeComponent();
        _studentService = studentService;
        BirthdayPicker.Date = new DateTime(2005, 1, 1);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_isEditMode)
        {
            Title = "Edit Student";
            SaveButton.Text = "Update Student";
            StudentIdEntry.IsEnabled = false;
            await LoadExistingStudentAsync();
        }
        else
        {
            Title = "Add Student";
            SaveButton.Text = "Save Student";
            StudentIdEntry.IsEnabled = true;
        }
    }

    private async Task LoadExistingStudentAsync()
    {
        try
        {
            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            var student = await _studentService.GetByIdAsync(_studentId);
            if (student is null)
            {
                await DisplayAlert("Not Found", "Student record could not be loaded.", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            _existingUserId = student.UserId;
            StudentIdEntry.Text = student.StudentId;
            FirstNameEntry.Text = student.FirstName;
            MiddleNameEntry.Text = student.MiddleName;
            SurnameEntry.Text = student.Surname;
            GenderPicker.SelectedItem = student.Gender;
            ProgramPicker.SelectedItem = student.Program;
            YearPicker.SelectedItem = student.YearLevel.ToString();
            BlocEntry.Text = student.Bloc;
            if (student.Birthday != default)
                BirthdayPicker.Date = student.Birthday;
            EmailEntry.Text = student.BuEmail;
            ContactEntry.Text = student.ContactNo;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load student: {ex.Message}", "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        string id = StudentIdEntry.Text?.Trim() ?? string.Empty;
        string firstName = FirstNameEntry.Text?.Trim() ?? string.Empty;
        string? middleName = string.IsNullOrWhiteSpace(MiddleNameEntry.Text) ? null : MiddleNameEntry.Text.Trim();
        string surname = SurnameEntry.Text?.Trim() ?? string.Empty;
        string gender = GenderPicker.SelectedItem?.ToString() ?? string.Empty;
        string program = ProgramPicker.SelectedItem?.ToString() ?? string.Empty;
        string bloc = BlocEntry.Text?.Trim().ToUpperInvariant() ?? string.Empty;
        string email = EmailEntry.Text?.Trim() ?? string.Empty;
        string contact = ContactEntry.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(id) ||
            string.IsNullOrWhiteSpace(firstName) ||
            string.IsNullOrWhiteSpace(surname) ||
            string.IsNullOrWhiteSpace(gender) ||
            string.IsNullOrWhiteSpace(program) ||
            YearPicker.SelectedItem is null ||
            string.IsNullOrWhiteSpace(bloc) ||
            string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Missing Fields", "Please fill in all required (*) fields.", "OK");
            return;
        }

        if (!email.EndsWith("@bicol-u.edu.ph", StringComparison.OrdinalIgnoreCase))
        {
            await DisplayAlert("Invalid Email", "BU Email must end with @bicol-u.edu.ph.", "OK");
            return;
        }

        var student = new Student
        {
            StudentId = id,
            UserId = _existingUserId,
            FirstName = firstName,
            MiddleName = middleName,
            Surname = surname,
            Gender = gender,
            Program = program,
            YearLevel = int.Parse(YearPicker.SelectedItem.ToString()!),
            Bloc = bloc,
            Birthday = BirthdayPicker.Date ?? new DateTime(2005, 1, 1),
            BuEmail = email,
            ContactNo = contact
        };

        try
        {
            SaveButton.IsEnabled = false;

            if (_isEditMode)
                await _studentService.UpdateAsync(student);
            else
                await _studentService.AddAsync(student);

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Save Failed", ex.Message, "OK");
        }
        finally
        {
            SaveButton.IsEnabled = true;
        }
    }

    private async void OnCancelClicked(object? sender, EventArgs e) =>
        await Shell.Current.GoToAsync("..");
}