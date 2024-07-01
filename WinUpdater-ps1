# Ensure running as administrator
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You need to run this script as an administrator!"
    exit
}

# Function to update installed applications using winget
function Update-Applications {
    Write-Output "Updating installed applications using winget..."
    # Check if winget is installed
    if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
        Write-Error "winget is not installed. Please install winget and try again."
        exit
    }
    
    # Update all applications using winget
    winget upgrade --all --silent --accept-source-agreements --accept-package-agreements
}

# Function to update Windows services and drivers
function Update-WindowsUpdates {
    Write-Output "Updating Windows services and drivers..."
    
    # Install the Windows Update module if not already installed
    if (-not (Get-Module -ListAvailable -Name PSWindowsUpdate)) {
        Install-Module -Name PSWindowsUpdate -Force -SkipPublisherCheck
    }
    
    Import-Module PSWindowsUpdate
    
    # Run Windows Update to install all available updates
    Get-WindowsUpdate -AcceptAll -Install -AutoReboot
}

# Function to update device drivers
function Update-DeviceDrivers {
    Write-Output "Updating device drivers..."
    
    # Using pnputil to update drivers
    $drivers = Get-WindowsUpdate -MicrosoftUpdate -Category "Drivers" -AcceptAll
    
    foreach ($driver in $drivers) {
        Write-Output "Updating driver: $($driver.Title)"
        Install-WindowsUpdate -KBArticleID $driver.KBArticleID -AcceptAll -AutoReboot
    }
}

# Main function to run updates
function Run-Updates {
    Update-Applications
    Update-WindowsUpdates
    Update-DeviceDrivers
}

# Run the updates
Run-Updates
