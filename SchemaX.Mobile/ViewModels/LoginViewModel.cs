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

    public void Reset()
    {
        Username = "";
        Password = "";
        ErrorMessage = "";
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        ErrorMessage = "";

        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter your username and password.";
            return;
        }

        try
        {
            bool ok = await _svc.LoginAsync(Username, Password);
            if (ok) await Shell.Current.GoToAsync("//students");
            else ErrorMessage = "Invalid username or password.";
        }
        catch (DataAccessException ex)
        {
#if DEBUG
            // Debug builds also show the technical reason, so we can find the real cause.
            ErrorMessage = $"{ex.Message} [{ex.InnerException?.Message}]";
#else
            ErrorMessage = ex.Message;
#endif
        }
        catch (Exception)
        {
            ErrorMessage = "Something went wrong. Please try again.";
        }
    }
}