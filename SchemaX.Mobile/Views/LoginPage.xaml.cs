using SchemaX.Mobile.ViewModels;

namespace SchemaX.Mobile.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _vm;

    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.Reset();
    }

    // 'Enter' on username jumps to password box
    private void OnUsernameCompleted(object? sender, EventArgs e) => PasswordEntry.Focus();

    // 'Enter' on password box logs in
    private void OnPasswordCompleted(object? sender, EventArgs e)
    {
        PasswordEntry.Unfocus();
        if (_vm.LoginCommand.CanExecute(null))
            _vm.LoginCommand.Execute(null);
    }
}