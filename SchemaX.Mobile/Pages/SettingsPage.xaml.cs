using SchemaX.Mobile.Services;

namespace SchemaX.Mobile.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly IStudentService _studentService;

    public SettingsPage(IStudentService studentService)
    {
        InitializeComponent();
        _studentService = studentService;
        ProviderLabel.Text = _studentService.GetType().Name;
    }

    private async void OnTestConnectionClicked(object? sender, EventArgs e)
    {
        try
        {
            TestConnectionButton.IsEnabled = false;
            ConnectionStatusLabel.Text = "Pinging service...";
            ConnectionStatusLabel.TextColor = Colors.Gray;

            bool isOnline = await _studentService.PingAsync();

            if (isOnline)
            {
                ConnectionStatusLabel.Text = "Connected (Online)";
                ConnectionStatusLabel.TextColor = Color.FromArgb("#2A9D8F");
            }
            else
            {
                ConnectionStatusLabel.Text = "Offline / Unreachable";
                ConnectionStatusLabel.TextColor = Color.FromArgb("#D9534F");
            }
        }
        catch (Exception ex)
        {
            ConnectionStatusLabel.Text = $"Error: {ex.Message}";
            ConnectionStatusLabel.TextColor = Color.FromArgb("#D9534F");
        }
        finally
        {
            TestConnectionButton.IsEnabled = true;
        }
    }

    private async void OnSignOutClicked(object? sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Sign Out", "Return to the Sign In screen?", "Sign Out", "Cancel");
        if (!confirm) return;

        await Shell.Current.GoToAsync("//login");
    }
}