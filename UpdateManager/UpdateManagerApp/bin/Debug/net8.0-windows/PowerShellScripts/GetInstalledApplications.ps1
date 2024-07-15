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

    # Filter out entries without a name
    $apps = $apps | Where-Object { $_.Name -ne $null -and $_.Name -ne "" }

    return $apps
}

# Execute the function and display the installed applications
$installedApps = Get-InstalledApplications

Write-Host "Installed Applications:"
$installedApps | Format-Table -AutoSize

# Optionally, export to a CSV file
$installedApps | Export-Csv -Path "InstalledApplications.csv" -NoTypeInformation

Write-Host "The list of installed applications has been exported to InstalledApplications.csv"
