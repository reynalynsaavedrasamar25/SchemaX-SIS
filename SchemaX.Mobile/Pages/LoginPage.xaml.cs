namespace SchemaX.Mobile.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnSignInClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//main/directory");
    }
}