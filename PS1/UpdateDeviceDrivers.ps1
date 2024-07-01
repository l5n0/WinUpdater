# Ensure running as administrator
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You need to run this script as an administrator!"
    exit
}

Write-Output "Updating device drivers..."
if (-not (Get-Module -ListAvailable -Name PSWindowsUpdate)) {
    Install-Module -Name PSWindowsUpdate -Force -SkipPublisherCheck
}
Import-Module PSWindowsUpdate
$drivers = Get-WindowsUpdate -MicrosoftUpdate -Category "Drivers" -AcceptAll
foreach ($driver in $drivers) {
    Write-Output "Updating driver: $($driver.Title)"
    Install-WindowsUpdate -KBArticleID $driver.KBArticleID -AcceptAll -AutoReboot
}
