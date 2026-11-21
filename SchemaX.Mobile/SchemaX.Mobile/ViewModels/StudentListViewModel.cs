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
    private int _loadVersion;                 // ignore outdated results
    private CancellationTokenSource? _searchCts;

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
    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string statusMessage = "";

    private FilterChip Chip(string kind, string label, string? value, bool selected = false) => new()
    {
        Kind = kind,
        Label = label,
        Value = value,
        IsSelected = selected,
        SelectCommand = SelectFilterCommand
    };

    // Run after text change in search bar
    partial void OnSearchTextChanged(string value) => _ = DebouncedSearchAsync();

    // Delay search until user stops typing for a moment, to avoid too many requests
    private async Task DebouncedSearchAsync()
    {
        _searchCts?.Cancel();
        _searchCts = new CancellationTokenSource();
        var token = _searchCts.Token;

        try
        {
            await Task.Delay(350, token);
            await LoadAsync();
        }
        catch (TaskCanceledException) { /* user typed again, ignore */ }
    }

    [RelayCommand]
    private async Task OpenDetailAsync(Student student)
    {
        await Shell.Current.GoToAsync("detail",
            new ShellNavigationQueryParameters { { "Student", student } });
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
    private async Task LoadAsync()
    {
        int version = ++_loadVersion;

        try
        {
            IsBusy = true;
            StatusMessage = "";

            var list = await _svc.GetStudentsAsync(SearchText, _program, _year);

            // new search started while waiting, so drop this result
            if (version != _loadVersion) return;

            Students.Clear();
            foreach (var s in list)
                Students.Add(s);

            StatusMessage = Students.Count == 0 ? "No students match your search." : "";
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