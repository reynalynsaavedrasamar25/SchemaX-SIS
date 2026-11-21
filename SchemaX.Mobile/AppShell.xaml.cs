using SchemaX.Mobile.Pages;

namespace SchemaX.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(StudentDetailPage), typeof(StudentDetailPage));
        Routing.RegisterRoute(nameof(StudentFormPage), typeof(StudentFormPage));
    }
}