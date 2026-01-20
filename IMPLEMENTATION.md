# Copy Helper - Implementation Details

## Overview
Copy Helper is a Windows system tray application built with .NET 10 and WPF that helps users quickly copy frequently used text snippets to the clipboard.

## Architecture

### Project Structure
```
CopyHelper/
├── Models/
│   └── CopyEntry.cs          # Data model for text entries
├── Services/
│   └── DataService.cs        # JSON file I/O for entries
├── Views/
│   ├── SettingsWindow.xaml   # Settings dialog UI
│   └── SettingsWindow.xaml.cs
├── Helpers/
│   └── IconHelper.cs         # System tray icon generation
├── App.xaml                  # Application definition
└── App.xaml.cs               # Application logic and tray icon management
```

### Key Components

#### 1. App.xaml.cs (Main Application Logic)
- **OnStartup**: Initializes the system tray icon without showing a main window
- **BuildContextMenu**: Dynamically builds the context menu with saved entries plus Settings and Exit options
- **CopyToClipboard**: Handles copying text to clipboard when an entry is clicked
- **SettingsItem_Click**: Opens the settings dialog
- **ExitItem_Click**: Cleans up and exits the application

#### 2. DataService
- **Location**: `%APPDATA%\CopyHelper\entries.json`
- **LoadEntries**: Reads entries from JSON file (returns empty list if file doesn't exist)
- **SaveEntries**: Writes entries to JSON file with pretty formatting

#### 3. SettingsWindow
- **UI**: DataGrid for managing entries with Name and Text columns
- **Features**:
  - Add/edit/delete entries inline
  - Save button: Validates and saves entries, updates context menu
  - Cancel button: Discards changes
  - Auto-cleanup: Removes empty entries on save

#### 4. IconHelper
- **Purpose**: Generates a simple system tray icon
- **Implementation**: Creates a 16x16 icon with blue background and white "C" text
- **Memory Safety**: Properly disposes GDI handles to prevent leaks

## Features

### System Tray Icon
- Displays a blue "C" icon in the Windows system tray
- Tooltip: "Copy Helper"
- No main window - runs entirely from the tray

### Context Menu
- **Dynamic entries**: Each saved entry appears as a menu item
- **Settings**: Opens the entry management dialog
- **Exit**: Closes the application

### Settings Dialog
- Grid-based editing interface
- Add rows for new entries
- Delete rows to remove entries
- Changes persist to JSON file on save
- Updates context menu immediately after saving

### Data Persistence
- JSON format for human readability
- Stored in user's AppData folder
- Automatic directory creation if needed
- Graceful handling of missing or corrupted files

## Technical Details

### Dependencies
- **H.NotifyIcon.Wpf** (v2.4.1): Modern system tray icon support for WPF
- **System.Drawing.Common**: Icon generation (included with .NET)
- **System.Text.Json**: JSON serialization (built-in)

### .NET Features Used
- **ImplicitUsings**: Reduces boilerplate code
- **Nullable reference types**: Improved null safety
- **File-scoped namespaces**: Cleaner code organization
- **Top-level statements**: Not used to keep Application subclass

### Platform Requirements
- **Target**: net10.0-windows
- **OS**: Windows only (uses Windows-specific features like system tray)
- **EnableWindowsTargeting**: Required for cross-platform build environments

## Usage Flow

1. **Application Start**
   - App starts with no visible window
   - System tray icon appears
   - Data service loads entries from JSON

2. **User Opens Settings**
   - Right-click tray icon
   - Click "Settings"
   - Settings dialog opens

3. **User Manages Entries**
   - Add/edit entries in grid
   - Click Save
   - Dialog closes
   - Context menu refreshes

4. **User Copies Text**
   - Right-click tray icon
   - Click an entry name
   - Text is copied to clipboard
   - No visual feedback (by design)

5. **Application Exit**
   - Right-click tray icon
   - Click "Exit"
   - Icon removed from tray
   - Application terminates

## Security Considerations

- ✅ No external network access
- ✅ Data stored locally in user's AppData
- ✅ No sensitive data handling
- ✅ Proper resource disposal (GDI handles)
- ✅ Exception handling for clipboard operations
- ✅ CodeQL scan passed with 0 alerts

## Build & Run

### Building
```bash
dotnet build CopyHelper.sln
# or
dotnet build CopyHelper.sln --configuration Release
```

### Running
```bash
dotnet run --project CopyHelper/CopyHelper.csproj
```

### Publishing (Single File)
```bash
dotnet publish CopyHelper/CopyHelper.csproj -c Release -r win-x64 --self-contained
```

## Future Enhancements (Not Implemented)

Potential improvements for future versions:
- Keyboard shortcuts for entries
- Import/export functionality
- Entry categories/folders
- Search/filter in settings
- Customizable icon
- Multi-monitor support testing
- Startup with Windows option
- Entry usage statistics
