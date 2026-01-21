using System.Reflection;
using System.Windows;
using CopyHelper.Models;
using CopyHelper.Services;

namespace CopyHelper.Views;

public partial class SettingsWindow : Window
{
    private readonly DataService _dataService;
    private List<CopyEntry> _entries;

    public event EventHandler? EntriesUpdated;

    public SettingsWindow(DataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        _entries = _dataService.LoadEntries();
        EntriesDataGrid.ItemsSource = _entries;
        
        // Set version text
        var version = Assembly.GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
            ?? Assembly.GetExecutingAssembly().GetName().Version?.ToString()
            ?? "Unknown";
        VersionTextBlock.Text = $"Version {version}";
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Remove empty entries
        _entries.RemoveAll(entry => string.IsNullOrWhiteSpace(entry.Name) && string.IsNullOrWhiteSpace(entry.Text));
        
        _dataService.SaveEntries(_entries);
        EntriesUpdated?.Invoke(this, EventArgs.Empty);
        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}
