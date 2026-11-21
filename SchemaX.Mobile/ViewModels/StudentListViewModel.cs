using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchemaX.Mobile.Services;
using SchemaX.SIS.Models;

namespace SchemaX.Mobile.ViewModels;

public partial class StudentListViewModel : ObservableObject
{
    private readonly IStudentService _svc;

    private string? _program;                 // current program filter (null = all)
    private int? _year;                       // current year filter (null = all)
    private int _loadVersion;                 // lets us ignore outdated results
    private CancellationTokenSource? _searchCts;
    private CancellationTokenSource? _loadCts;      // cancels the previous load when a new one starts

    public StudentListViewModel(IStudentService svc)
    {
        _svc = svc;

        ProgramFilters = new ObservableCollection<FilterChip>
        {
            Chip("program", "All Programs", null, selected: true),
            Chip("program", "BSCS", "BSCS"),
            Chip("program", "BSIT", "BSIT"),
        };

        YearFilters = new ObservableCollection<FilterChip>
        {
            Chip("year", "All Years", null, selected: true),
            Chip("year", "1st Year", "1"),
            Chip("year", "2nd Year", "2"),
            Chip("year", "3rd Year", "3"),
            Chip("year", "4th Year", "4"),
            Chip("year", "5th Year", "5"),
        };
    }

    public ObservableCollection<Student> Students { get; } = new();
    public ObservableCollection<FilterChip> ProgramFilters { get; }
    public ObservableCollection<FilterChip> YearFilters { get; }

    [ObservableProperty] private string searchText = "";
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasStatus))]
    private string statusMessage = "";

    public bool HasStatus => !string.IsNullOrEmpty(StatusMessage);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowSpinner))]
    private bool isBusy;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowSpinner))]
    private bool isRefreshing;

    // center spinner only for normal loads; pull-to-refresh has independent spinner
    public bool ShowSpinner => IsBusy && !IsRefreshing;

    public string CountText => Students.Count == 1 ? "1 student" : $"{Students.Count} students";

    private FilterChip Chip(string kind, string label, string? value, bool selected = false) => new()
    {
        Kind = kind,
        Label = label,
        Value = value,
        IsSelected = selected,
        SelectCommand = SelectFilterCommand
    };

    // run every time the search text changes, but only search after a short delay
    partial void OnSearchTextChanged(string value) => _ = DebouncedSearchAsync();

    // wait 350 ms after last keystroke to avoid query spamming the service
    private async Task DebouncedSearchAsync()
    {
        _searchCts?.Cancel();
        _searchCts = new CancellationTokenSource();
        var token = _searchCts.Token;

        try
        {
            await Task.Delay(150, token);
            await LoadAsync();
        }
        catch (TaskCanceledException) { /* user typed again, ignore */ }
    }

    [RelayCommand(AllowConcurrentExecutions = true)]
    private async Task SelectFilterAsync(FilterChip chip)
    {
        var group = chip.Kind == "program" ? ProgramFilters : YearFilters;
        foreach (var c in group)
            c.IsSelected = c == chip;

        if (chip.Kind == "program")
            _program = chip.Value;
        else
            _year = chip.Value is null ? null : int.Parse(chip.Value);

        await LoadAsync();
    }

    [RelayCommand]
    private async Task OpenDetailAsync(Student student)
    {
        await Shell.Current.GoToAsync("detail",
            new ShellNavigationQueryParameters { { "Student", student } });
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        try { await LoadAsync(); }
        finally { IsRefreshing = false; }
    }

    [RelayCommand]
    private async Task LogoutAsync() => await Shell.Current.GoToAsync("//login");

    [RelayCommand]
    private async Task LoadAsync()
    {
        int version = ++_loadVersion;

        // cancel the previous load and give this one an 8-second limit
        _loadCts?.Cancel();
        _loadCts = new CancellationTokenSource(TimeSpan.FromSeconds(8));
        var ct = _loadCts.Token;

        try
        {
            IsBusy = true;
            StatusMessage = "";

            // run the query on a background thread so the screen can never freeze
            string search = SearchText;
            string? program = _program;
            int? year = _year;
            var list = await Task.Run(() => _svc.GetStudentsAsync(search, program, year, ct), ct);

            // newer search started while waiting, so drop this result
            if (version != _loadVersion) return;

            Students.Clear();
            foreach (var s in list)
                Students.Add(s);

            OnPropertyChanged(nameof(CountText));
            StatusMessage = Students.Count == 0 ? "No students match your search." : "";
        }
        catch (OperationCanceledException)
        {
            // Replaced by a newer load: ignore. Still the newest load: it hit the 8-second limit.
            if (version == _loadVersion)
                StatusMessage = "The database took too long to respond. Pull down to retry.";
        }
        catch (DataAccessException ex)
        {
            if (version == _loadVersion)
                StatusMessage = ex.Message;   // "Cannot reach the database. Check that MySQL is running."
        }
        catch (Exception)
        {
            if (version == _loadVersion)
                StatusMessage = "Could not load students. Please try again.";
        }
        finally
        {
            if (version == _loadVersion)
                IsBusy = false;
        }
    }
}