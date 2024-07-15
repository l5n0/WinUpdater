param (
    [string]$driverName
)

# Ensure running as administrator
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You need to run this script as an administrator!"
    exit
}

Write-Output "Updating specific driver: $driverName"
if (-not (Get-Module -ListAvailable -Name PSWindowsUpdate)) {
    Install-Module -Name PSWindowsUpdate -Force -SkipPublisherCheck
}
Import-Module PSWindowsUpdate
Get-WindowsUpdate -Category Drivers -Title $driverName -Install -AcceptAll -AutoReboot
