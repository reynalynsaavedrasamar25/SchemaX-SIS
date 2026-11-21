using SchemaX.Mobile.Models;
using SchemaX.Mobile.Services;

namespace SchemaX.Mobile.Pages;

public partial class DirectoryPage : ContentPage
{
    private readonly IStudentService _studentService;
    private string _selectedProgram = "All";
    private int _selectedYear = 0;
    private CancellationTokenSource? _searchCts;

    public DirectoryPage(IStudentService studentService)
    {
        InitializeComponent();
        _studentService = studentService;
        UpdateChipStyles();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadStudentsAsync();
    }

    private async Task LoadStudentsAsync(CancellationToken ct = default)
    {
        try
        {
            int? yearFilter = _selectedYear > 0 ? _selectedYear : null;
            var students = await _studentService.GetStudentsAsync(
                StudentSearchBar.Text,
                _selectedProgram,
                yearFilter,
                ct);

            if (!ct.IsCancellationRequested)
                StudentsCollection.ItemsSource = students;
        }
        catch (OperationCanceledException)
        {
            // Ignored when a newer search keystroke cancels the previous request
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load students: {ex.Message}", "OK");
        }
        finally
        {
            DirectoryRefreshView.IsRefreshing = false;
        }
    }

    private async void OnSearchTextChanged(object? sender, TextChangedEventArgs e)
    {
        _searchCts?.Cancel();
        _searchCts = new CancellationTokenSource();
        var token = _searchCts.Token;

        try
        {
            await Task.Delay(300, token);
            await LoadStudentsAsync(token);
        }
        catch (OperationCanceledException) { }
    }

    private async void OnProgramChipClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            _selectedProgram = btn.Text;
            UpdateChipStyles();
            await LoadStudentsAsync();
        }
    }

    private async void OnYearChipClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn && int.TryParse(btn.CommandParameter?.ToString(), out int yr))
        {
            _selectedYear = yr;
            UpdateChipStyles();
            await LoadStudentsAsync();
        }
    }

    private void UpdateChipStyles()
    {
        Color activeBg = (Color)Application.Current!.Resources["SchemaPrimary"];
        Color inactiveBg = (Color)Application.Current!.Resources["BorderLight"];
        Color inactiveText = (Color)Application.Current!.Resources["TextPrimaryLight"];

        foreach (var child in ProgramChipsLayout.Children.OfType<Button>())
        {
            bool active = child.Text.Equals(_selectedProgram, StringComparison.OrdinalIgnoreCase);
            child.BackgroundColor = active ? activeBg : inactiveBg;
            child.TextColor = active ? Colors.White : inactiveText;
        }

        foreach (var child in YearChipsLayout.Children.OfType<Button>())
        {
            int yr = int.TryParse(child.CommandParameter?.ToString(), out int v) ? v : 0;
            bool active = yr == _selectedYear;
            child.BackgroundColor = active ? activeBg : inactiveBg;
            child.TextColor = active ? Colors.White : inactiveText;
        }
    }

    private async void OnRefreshing(object? sender, EventArgs e) => await LoadStudentsAsync();

    private async void OnAddClicked(object? sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(StudentFormPage));

    private async void OnStudentSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Student student)
        {
            StudentsCollection.SelectedItem = null;
            await Shell.Current.GoToAsync($"{nameof(StudentDetailPage)}?studentId={student.StudentId}");
        }
    }
}