using SchemaX.Mobile.ViewModels;

namespace SchemaX.Mobile.Views;

public partial class StudentListPage : ContentPage
{
    private readonly StudentListViewModel _vm;

    public StudentListPage(StudentListViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadCommand.ExecuteAsync(null);
    }
}