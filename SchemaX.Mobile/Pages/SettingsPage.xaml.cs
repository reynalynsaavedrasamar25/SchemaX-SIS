namespace SchemaX.Mobile.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private async void OnSignOutClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//login");
    }
}