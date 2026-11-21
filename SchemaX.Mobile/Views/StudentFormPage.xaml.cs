
using SchemaX.Mobile.ViewModels;

namespace SchemaX.Mobile.Views;

public partial class StudentFormPage : ContentPage
{
    public StudentFormPage(StudentFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
