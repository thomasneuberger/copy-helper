# Enable-Autostart.ps1
# This script adds CopyHelper to the current user's startup programs

param(
    [string]$ExecutablePath
)

# If no executable path is provided, use the current directory
if (-not $ExecutablePath) {
    $ExecutablePath = Join-Path $PSScriptRoot "CopyHelper.exe"
}

# Verify the executable exists
if (-not (Test-Path $ExecutablePath)) {
    Write-Error "CopyHelper.exe not found at: $ExecutablePath"
    Write-Host "Please provide the full path to CopyHelper.exe as a parameter."
    Write-Host "Example: .\enable-autostart.ps1 -ExecutablePath 'C:\Path\To\CopyHelper.exe'"
    exit 1
}

# Get the full path
$ExecutablePath = Resolve-Path $ExecutablePath

# Registry path for current user startup programs
$registryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
$appName = "CopyHelper"

try {
    # Add the application to startup
    Set-ItemProperty -Path $registryPath -Name $appName -Value "`"$ExecutablePath`"" -Force
    Write-Host "✓ CopyHelper has been added to startup successfully!" -ForegroundColor Green
    Write-Host "  Path: $ExecutablePath" -ForegroundColor Gray
    Write-Host ""
    Write-Host "The application will now start automatically when you log in." -ForegroundColor Cyan
} catch {
    Write-Error "Failed to add CopyHelper to startup: $_"
    exit 1
}
