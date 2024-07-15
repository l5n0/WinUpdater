# GetInstalledApplications.ps1
# This script retrieves installed applications on a Windows machine

function Get-InstalledApplications {
    $apps = @()
    
    $registryPaths = @(
        'HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*',
        'HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\*',
        'HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*'
    )

    foreach ($path in $registryPaths) {
        $apps += Get-ItemProperty -Path $path -ErrorAction SilentlyContinue | ForEach-Object {
            [PSCustomObject]@{
                Name         = $_.DisplayName
                Version      = $_.DisplayVersion
                Publisher    = $_.Publisher
                InstallDate  = $_.InstallDate
                UninstallCmd = $_.UninstallString
            }
        }
    }

    # Filter out entries without a name or version
    $apps = $apps | Where-Object { $_.Name -ne $null -and $_.Name -ne "" -and $_.Version -ne $null -and $_.Version -ne "" }

    # Group by Name and select the latest version for each group
    $uniqueApps = $apps | Sort-Object Name, Version -Descending | Group-Object Name | ForEach-Object { $_.Group | Sort-Object Version -Descending | Select-Object -First 1 }

    return $uniqueApps
}

# Execute the function and display the installed applications
$installedApps = Get-InstalledApplications

Write-Host "Installed Applications:"
$installedApps | Format-Table -AutoSize

# Optionally, export to a CSV file
$installedApps | Export-Csv -Path "InstalledApplications.csv" -NoTypeInformation

Write-Host "The list of installed applications has been exported to InstalledApplications.csv"
