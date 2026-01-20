using System.Windows;
using H.NotifyIcon;
using CopyHelper.Services;
using CopyHelper.Views;
using CopyHelper.Models;
using CopyHelper.Helpers;
using System.Windows.Controls;

namespace CopyHelper;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private TaskbarIcon? _notifyIcon;
    private DataService? _dataService;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _dataService = new DataService();

        // Create the NotifyIcon
        _notifyIcon = new TaskbarIcon();
        _notifyIcon.ToolTip = "Copy Helper";
        
        // Create icon
        _notifyIcon.Icon = IconHelper.CreateIcon("C", System.Drawing.Color.DodgerBlue);

        // Build context menu
        BuildContextMenu();

        _notifyIcon.ForceCreate();
    }

    private void BuildContextMenu()
    {
        if (_notifyIcon == null || _dataService == null)
            return;

        var contextMenu = new ContextMenu();

        // Load and add entry items
        var entries = _dataService.LoadEntries();
        foreach (var entry in entries)
        {
            var menuItem = new MenuItem { Header = entry.Name };
            var entryText = entry.Text; // Capture for closure
            menuItem.Click += (s, e) => CopyToClipboard(entryText);
            contextMenu.Items.Add(menuItem);
        }

        // Add separator if there are entries
        if (entries.Count > 0)
        {
            contextMenu.Items.Add(new Separator());
        }

        // Settings menu item
        var settingsItem = new MenuItem { Header = "Settings" };
        settingsItem.Click += SettingsItem_Click;
        contextMenu.Items.Add(settingsItem);

        // Exit menu item
        var exitItem = new MenuItem { Header = "Exit" };
        exitItem.Click += ExitItem_Click;
        contextMenu.Items.Add(exitItem);

        _notifyIcon.ContextMenu = contextMenu;
    }

    private void CopyToClipboard(string text)
    {
        try
        {
            Clipboard.SetText(text);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to copy to clipboard: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SettingsItem_Click(object? sender, EventArgs e)
    {
        if (_dataService == null)
            return;

        var settingsWindow = new SettingsWindow(_dataService);
        settingsWindow.EntriesUpdated += (s, e) => BuildContextMenu();
        settingsWindow.ShowDialog();
    }

    private void ExitItem_Click(object? sender, EventArgs e)
    {
        _notifyIcon?.Dispose();
        Current.Shutdown();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _notifyIcon?.Dispose();
        base.OnExit(e);
    }
}

