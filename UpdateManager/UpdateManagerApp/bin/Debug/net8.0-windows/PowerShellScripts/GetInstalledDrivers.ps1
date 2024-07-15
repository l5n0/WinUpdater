# GetInstalledDrivers.ps1
# This script retrieves installed drivers on a Windows machine

function Get-InstalledDrivers {
    $drivers = Get-WmiObject Win32_PnPSignedDriver | Select-Object DeviceName, DriverVersion

    # Filter out entries without a name or version
    $drivers = $drivers | Where-Object { $_.DeviceName -ne $null -and $_.DeviceName -ne "" -and $_.DriverVersion -ne $null -and $_.DriverVersion -ne "" }

    # Group by DeviceName and select the latest version for each group
    $uniqueDrivers = $drivers | Sort-Object DeviceName, DriverVersion -Descending | Group-Object DeviceName | ForEach-Object { $_.Group | Sort-Object DriverVersion -Descending | Select-Object -First 1 }

    return $uniqueDrivers
}

# Execute the function and display the installed drivers
$installedDrivers = Get-InstalledDrivers

Write-Host "Installed Drivers:"
$installedDrivers | Format-Table -AutoSize

# Optionally, export to a CSV file
$installedDrivers | Export-Csv -Path "InstalledDrivers.csv" -NoTypeInformation

Write-Host "The list of installed drivers has been exported to InstalledDrivers.csv"
