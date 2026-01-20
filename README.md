# copy-helper

A little Windows tool to help copying frequently used texts

## Features

- **System Tray Icon**: Runs in the background with a system tray icon
- **No Main Window**: Minimalist design with no main window, only accessible via the system tray
- **Quick Copy**: Click on any saved entry from the context menu to copy its text to clipboard
- **Settings Dialog**: Manage your frequently used text entries (name + text)
- **Persistent Storage**: Entries are stored in JSON format in your AppData folder

## Requirements

- Windows OS
- .NET 10.0

## How to Use

1. Run the application - a blue "C" icon will appear in your system tray
2. Right-click the icon to open the context menu
3. Click "Settings" to manage your text entries
4. Add entries with a name and the text you want to copy
5. Click "Save" to save your entries
6. Your saved entries will appear in the context menu
7. Click any entry name to copy its text to the clipboard

## Building

```bash
dotnet build CopyHelper.sln
```

## Running

```bash
dotnet run --project CopyHelper/CopyHelper.csproj
```

## Data Storage

Entries are stored in: `%APPDATA%\CopyHelper\entries.json`
