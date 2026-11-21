using SchemaX.Mobile.Views;

namespace SchemaX.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("detail", typeof(StudentDetailPage));
            Routing.RegisterRoute("studentform", typeof(StudentFormPage));
        }
    }
}