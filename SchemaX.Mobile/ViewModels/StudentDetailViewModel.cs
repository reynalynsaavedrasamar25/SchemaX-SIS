using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchemaX.SIS.Models;

namespace SchemaX.Mobile.ViewModels;

[QueryProperty(nameof(Student), "Student")]
public partial class StudentDetailViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Initials))]
    private Student student = new();

    // Convert initials for avatar circle
    public string Initials
    {
        get
        {
            string f = Student.FirstName.Trim();
            string l = Student.Surname.Trim();
            string first = f.Length > 0 ? char.ToUpper(f[0]).ToString() : "";
            string last = l.Length > 0 ? char.ToUpper(l[0]).ToString() : "";
            return first + last;
        }
    }

    [RelayCommand]
    private async Task GoBackAsync() => await Shell.Current.GoToAsync("..");
}