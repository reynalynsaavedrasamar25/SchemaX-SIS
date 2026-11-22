
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IntelliJ.Lang.Annotations;
using SchemaX.Mobile.Services;
using SchemaX.SIS.Models;

namespace SchemaX.Mobile.ViewModels;

[QueryProperty(nameof(Student), "Student")]
public partial class StudentDetailViewModel : ObservableObject
{
    private readonly IStudentService _service;

    public StudentDetailViewModel(IStudentService service)
    {
        _service = service;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Initials))]
    private Student student = new();

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string statusMessage = "";

    public string Initials
    {
        get
        {
            string f = Student.FirstName.Trim();
            string l = Student.Surname.Trim();

            string first = f.Length > 0
                ? char.ToUpper(f[0]).ToString() : "";

            string last = l.Length > 0
                ? char.ToUpper(l[0]).ToString() : "";

            return first + last;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task EditAsync()
    {
        await Shell.Current.GoToAsync(
            "studentform",
            new ShellNavigationQueryParameters
            {
                { "Student", Student }
            });
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (IsBusy)
            return;

        bool confirm = await Shell.Current.DisplayAlertAsync(
            "Delete Student",
            $"Are you sure you want to delete {Student.FullName}?",
            "Delete",
            "Cancel");

        if (!confirm)
            return;

        IsBusy = true;

        try
        {
            await _service.DeleteAsync(Student.StudentId);

            await Shell.Current.DisplayAlertAsync(
                "Success",
                "Student deleted successfully.",
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
            StatusMessage = "Could not delete student.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
