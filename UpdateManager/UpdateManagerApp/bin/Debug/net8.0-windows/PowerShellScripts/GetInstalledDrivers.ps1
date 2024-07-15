# GetInstalledDrivers.ps1
# This script retrieves installed drivers on a Windows machine

function Get-InstalledDrivers {
    $drivers = @()

    $driverQuery = Get-WmiObject Win32_PnPSignedDriver

    foreach ($driver in $driverQuery) {
        if ($driver.DeviceName -and $driver.DriverVersion -and $driver.Manufacturer) {
            $drivers += [PSCustomObject]@{
                Name          = $driver.DeviceName
                DriverVersion = $driver.DriverVersion
                Manufacturer  = $driver.Manufacturer
                InstallDate   = $driver.InstallDate
            }
        }
    }

    # Filter out drivers with empty or null values
    $filteredDrivers = $drivers | Where-Object {
        $_.Name -ne "" -and $_.DriverVersion -ne "" -and $_.Manufacturer -ne ""
    }

    # Group by Name and select the first entry for each group
    $uniqueDrivers = $filteredDrivers | Group-Object Name | ForEach-Object { $_.Group[0] }

    return $uniqueDrivers
}

# Execute the function and display the installed drivers
$installedDrivers = Get-InstalledDrivers

Write-Host "Installed Drivers:"
$installedDrivers | Format-Table -AutoSize

# Optionally, export to a CSV file
$installedDrivers | Export-Csv -Path "InstalledDrivers.csv" -NoTypeInformation

Write-Host "The list of installed drivers has been exported to InstalledDrivers.csv"
