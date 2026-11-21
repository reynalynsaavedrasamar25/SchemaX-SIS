using SchemaX.Mobile.ViewModels;

namespace SchemaX.Mobile.Views;

public partial class StudentDetailPage : ContentPage
{
    public StudentDetailPage(StudentDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}