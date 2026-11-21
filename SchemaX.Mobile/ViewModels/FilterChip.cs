using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SchemaX.Mobile.ViewModels;

public partial class FilterChip : ObservableObject
{
    public string Kind { get; init; } = "";      // "program" or "year"
    public string Label { get; init; } = "";     // button text
    public string? Value { get; init; }          // null = "All"
    public ICommand? SelectCommand { get; init; }

    [ObservableProperty] private bool isSelected;
}