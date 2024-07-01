param (
    [string]$appName
)

# Ensure running as administrator
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    Write-Warning "You need to run this script as an administrator!"
    exit
}

if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
    Write-Error "winget is not installed. Please install winget and try again."
    exit
}

Write-Output "Updating application: $appName"
winget upgrade --name $appName --silent --accept-source-agreements --accept-package-agreements
