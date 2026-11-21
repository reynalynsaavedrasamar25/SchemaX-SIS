using SchemaX.Mobile.ViewModels;
using SchemaX.SIS.Models;

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

    private async void OnStudentSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not Student tapped)
            return;

        ((CollectionView)sender!).SelectedItem = null;

        await _vm.OpenDetailCommand.ExecuteAsync(tapped);
    }
}