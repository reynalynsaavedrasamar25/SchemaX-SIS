using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchemaX.Mobile.Services;
using SchemaX.SIS.Models;

namespace SchemaX.Mobile.ViewModels;

public partial class StudentListViewModel : ObservableObject
{
    private readonly IStudentService _svc;
    public StudentListViewModel(IStudentService svc) => _svc = svc;

    public ObservableCollection<Student> Students { get; } = new();

    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string statusMessage = "";

    [RelayCommand]
    private async Task LoadAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            StatusMessage = "";

            var list = await _svc.GetStudentsAsync();

            Students.Clear();
            foreach (var s in list)
                Students.Add(s);

            if (Students.Count == 0)
                StatusMessage = "No students found.";
        }
        catch (Exception)
        {
            StatusMessage = "Could not load students. Please try again.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}