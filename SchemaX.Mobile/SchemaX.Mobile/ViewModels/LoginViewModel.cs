using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchemaX.Mobile.Services;

namespace SchemaX.Mobile.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IStudentService _svc;
    public LoginViewModel(IStudentService svc) => _svc = svc;

    [ObservableProperty] private string username = "";
    [ObservableProperty] private string password = "";
    [ObservableProperty] private string errorMessage = "";

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter your username and password.";
            return;
        }

        bool ok = await _svc.LoginAsync(Username, Password);
        if (ok) await Shell.Current.GoToAsync("//students");
        else ErrorMessage = "Invalid username or password.";
    }
}