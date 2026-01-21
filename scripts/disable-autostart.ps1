# Disable-Autostart.ps1
# This script removes CopyHelper from the current user's startup programs

# Registry path for current user startup programs
$registryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
$appName = "CopyHelper"

try {
    # Check if the entry exists
    $currentValue = Get-ItemProperty -Path $registryPath -Name $appName -ErrorAction SilentlyContinue
    
    if ($currentValue) {
        # Remove the application from startup
        Remove-ItemProperty -Path $registryPath -Name $appName -Force
        Write-Host "✓ CopyHelper has been removed from startup successfully!" -ForegroundColor Green
        Write-Host "  Previous path: $($currentValue.$appName)" -ForegroundColor Gray
        Write-Host ""
        Write-Host "The application will no longer start automatically when you log in." -ForegroundColor Cyan
    } else {
        Write-Host "ℹ CopyHelper is not currently set to start automatically." -ForegroundColor Yellow
    }
} catch {
    Write-Error "Failed to remove CopyHelper from startup: $_"
    exit 1
}
