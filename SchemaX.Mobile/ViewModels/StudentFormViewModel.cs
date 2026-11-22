
using Android.Renderscripts;
using Android.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliJ.Lang.Annotations;
using SchemaX.Mobile.Services;
using SchemaX.SIS.Models;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using static Android.Provider.ContactsContract.CommonDataKinds;

namespace SchemaX.Mobile.ViewModels;

[QueryProperty(nameof(Student), "Student")]
public partial class StudentFormViewModel : ObservableObject
{
    private readonly IStudentService _service;

    public StudentFormViewModel(IStudentService service)
    {
        _service = service;
    }

    [ObservableProperty]
    private Student? student;

    [ObservableProperty]
    private string studentId = "";

    [ObservableProperty]
    private string firstName = "";

    [ObservableProperty]
    private string middleName = "";

    [ObservableProperty]
    private string surname = "";

    [ObservableProperty]
    private string gender = "";

    [ObservableProperty]
    private string program = "";

    [ObservableProperty]
    private int yearLevel;

    [ObservableProperty]
    private string bloc = "";

    [ObservableProperty]
    private DateTime birthday = new DateTime(2000, 1, 1);

    [ObservableProperty]
    private string buEmail = "";

    [ObservableProperty]
    private string contactNo = "";

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string statusMessage = "";

    public bool IsEditing => Student != null;
    public bool IsNotBusy => !IsBusy;
    public DateTime Today => DateTime.Today;

    public string PageTitle => IsEditing ? "Edit Student" : "Add Student";
    public string SaveButtonText => IsEditing ? "Save Changes" : "Add Student";

    public ObservableCollection<string> Genders { get; } =
        new() { "Male", "Female" };

    public ObservableCollection<string> Programs { get; } =
        new() { "BSCS", "BSIT" };

    public ObservableCollection<int> YearLevels { get; } =
        new() { 1, 2, 3, 4, 5 };

    partial void OnStudentChanged(Student? value)
    {
        if (value == null)
            return;

        StudentId = value.StudentId;
        FirstName = value.FirstName;
        MiddleName = value.MiddleName ?? "";
        Surname = value.Surname;
        Gender = value.Gender;
        Program = value.Program;
        YearLevel = value.YearLevel;
        Bloc = value.Bloc;
        Birthday = value.Birthday;
        BuEmail = value.BuEmail;
        ContactNo = value.ContactNo;

        OnPropertyChanged(nameof(IsEditing));
        OnPropertyChanged(nameof(PageTitle));
        OnPropertyChanged(nameof(SaveButtonText));
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrWhiteSpace(StudentId) ||
            string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(Surname) ||
            string.IsNullOrWhiteSpace(Gender) ||
            string.IsNullOrWhiteSpace(Program) ||
            YearLevel < 1 ||
            string.IsNullOrWhiteSpace(Bloc) ||
            string.IsNullOrWhiteSpace(BuEmail) ||
            string.IsNullOrWhiteSpace(ContactNo))
        {
            StatusMessage = "Please complete all required fields.";
            return;
        }

        if (!BuEmail.Trim().EndsWith("@bicol-u.edu.ph",
                StringComparison.OrdinalIgnoreCase))
        {
            StatusMessage = "Please enter a valid BU email address.";
            return;
        }

        if (Birthday.Date > DateTime.Today)
        {
            StatusMessage = "Birthday cannot be in the future.";
            return;
        }

        IsBusy = true;
        OnPropertyChanged(nameof(IsNotBusy));

        try
        {
            var record = new Student
            {
                StudentId = StudentId.Trim(),
                FirstName = FirstName.Trim(),
                MiddleName = string.IsNullOrWhiteSpace(MiddleName)
                    ? null : MiddleName.Trim(),
                Surname = Surname.Trim(),
                Gender = Gender,
                Program = Program,
                YearLevel = YearLevel,
                Bloc = Bloc.Trim(),
                Birthday = Birthday.Date,
                BuEmail = BuEmail.Trim(),
                ContactNo = ContactNo.Trim()
            };

            if (IsEditing)
                await _service.UpdateAsync(record);
            else
                await _service.AddAsync(record);

            await Shell.Current.DisplayAlertAsync(
                "Success",
                IsEditing ? "Student updated successfully."
                 : "Student added successfully.",
                "OK");

            await Shell.Current.GoToAsync("..");
        }
        catch (DataAccessException ex)
        {
            StatusMessage = ex.Message;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
            StatusMessage = "Could not save student. Please try again.";
        }
        finally
        {
            IsBusy = false;
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
