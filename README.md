# WinUpdater
Windows Updater

Please don't be so hard on me this is my first software.

I built this application so that people can update the applications they have to the latest version. At the moment it works, but the GUI doesn't look good. This will be the next step to make the GUI usable.

# Installation Guide for UpdateManagerApp

Follow these steps to download, build, and install the UpdateManagerApp on your Windows machine.

## Prerequisites

Before you begin, ensure you have the following dependencies installed on your machine:

1. **Git**: Used to clone the repository.
   - Download and install Git from [here](https://git-scm.com/downloads).
   - Verify the installation by running `git --version` in a command prompt.

2. **.NET SDK**: Required to build the application.
   - Download and install the .NET SDK from [here](https://download.visualstudio.microsoft.com/download/pr/b6f19ef3-52ca-40b1-b78b-0712d3c8bf4d/426bd0d376479d551ce4d5ac0ecf63a5/dotnet-sdk-8.0.302-win-x64.exe).
   - Verify the installation by running `dotnet --version` in a command prompt.

## Steps to Install

### Step 1: Clone the Repository

1. Open a command prompt.
2. Navigate to the directory where you want to clone the repository.
3. Run the following command to clone the repository:

   ```sh
   git clone https://github.com/l5n0/WinUpdater.git
   ```

### Step 2: Navigate to the Project Directory

Change to the project directory by running:

```sh
cd WinUpdater\UpdateManager\UpdateManagerApp
```

### Step 3: Restore Dependencies
Restore the project dependencies by running:
```sh
dotnet restore
```

### Step 4: Build the Project
Build the project by running:
```sh
dotnet publish -c Release -r win-x64 --self-contained
```

This will create a self-contained executable in the `publish` folder.

### Step 5: Run the Application
1. Navigate to the publish folder:
```sh
cd bin\Release\net8.0-windows\win-x64\publish
```

2. Run the application by executing the EXE file:
```sh
.\UpdateManagerApp.exe
```
### Step 6: (Optional) Create a Shortcut
To create a desktop shortcut for easy access, follow these steps:

1. Open a command prompt as administrator.

2. Run the following PowerShell command to create a shortcut on the desktop:
```ps
$WScriptShell = New-Object -ComObject WScript.Shell
$desktopPath = [System.IO.Path]::Combine([System.Environment]::GetFolderPath("Desktop"), "UpdateManagerApp.lnk")
$shortcut = $WScriptShell.CreateShortcut($desktopPath)
$shortcut.TargetPath = "$pwd\UpdateManagerApp.exe"
$shortcut.WorkingDirectory = "$pwd"
$shortcut.Save()
```
Ensure that you are in the `publish` folder when running this command, so `$pwd` points to the correct location of `UpdateManagerApp.exe.`