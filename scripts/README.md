# Autostart Scripts

This directory contains PowerShell scripts to manage CopyHelper's autostart functionality on Windows.

## Scripts

### enable-autostart.ps1
Adds CopyHelper to the current user's startup programs. The application will automatically start when you log in to Windows.

**Usage:**
```powershell
# Using default path (looks for CopyHelper.exe in the same directory as the script)
.\enable-autostart.ps1

# Specifying a custom path
.\enable-autostart.ps1 -ExecutablePath "C:\Path\To\CopyHelper.exe"
```

**What it does:**
- Verifies that CopyHelper.exe exists at the specified location
- Adds an entry to the Windows registry at `HKCU:\Software\Microsoft\Windows\CurrentVersion\Run`
- Provides feedback on success or failure

### disable-autostart.ps1
Removes CopyHelper from the current user's startup programs.

**Usage:**
```powershell
.\disable-autostart.ps1
```

**What it does:**
- Checks if CopyHelper is currently set to autostart
- Removes the registry entry if it exists
- Provides feedback on the operation result

## Requirements

- Windows Operating System
- PowerShell (included with Windows)
- Administrator privileges are NOT required (operates on current user only)

## Notes

- These scripts modify the registry for the **current user only** (HKCU hive)
- Changes take effect on the next user login
- The scripts do not require elevated privileges
- You can verify the registry entry manually at: `HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run`
