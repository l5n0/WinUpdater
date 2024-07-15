using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UpdateManagerApp
{
    public static class UpdateActions
    {
        private static string scriptBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PowerShellScripts");

        public static void UpdateAllApplications()
        {
            ExecuteUpdate(Path.Combine(scriptBasePath, "UpdateAllApplications.ps1"));
        }

        public static void UpdateSpecificApplication(string appName)
        {
            ExecuteUpdate(Path.Combine(scriptBasePath, "UpdateSpecificApplication.ps1"), appName);
        }

        public static void UpdateWindowsServicesAndDrivers()
        {
            ExecuteUpdate(Path.Combine(scriptBasePath, "UpdateWindowsServicesAndDrivers.ps1"));
        }

        public static void UpdateAllDrivers()
        {
            ExecuteUpdate(Path.Combine(scriptBasePath, "UpdateAllDrivers.ps1"));
        }

        public static void UpdateSpecificDriver(string driverName)
        {
            ExecuteUpdate(Path.Combine(scriptBasePath, "UpdateSpecificDriver.ps1"), driverName);
        }

        public static List<ApplicationInfo> GetInstalledApplications()
        {
            var applications = new List<ApplicationInfo>();
            string scriptPath = Path.Combine(scriptBasePath, "GetInstalledApplications.ps1");

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process() { StartInfo = startInfo })
            {
                process.Start();
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        var columns = line.Split(new[] { ' ' }, 5);
                        if (columns.Length >= 2)
                        {
                            applications.Add(new ApplicationInfo
                            {
                                Name = columns[0],
                                CurrentVersion = columns[1]
                            });
                        }
                    }
                }

                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }

            return applications;
        }

        public static List<DriverInfo> GetInstalledDrivers()
        {
            var drivers = new List<DriverInfo>();
            string scriptPath = Path.Combine(scriptBasePath, "GetInstalledDrivers.ps1");

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process() { StartInfo = startInfo })
            {
                process.Start();
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        var columns = line.Split(new[] { ' ' }, 5);
                        if (columns.Length >= 2)
                        {
                            drivers.Add(new DriverInfo
                            {
                                Name = columns[0],
                                CurrentVersion = columns[1]
                            });
                        }
                    }
                }

                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }

            return drivers;
        }

        private static void ExecuteUpdate(string scriptPath, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\" {arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process() { StartInfo = startInfo })
            {
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (line != null)
                    {
                        Console.WriteLine(line); // Replace this with logging if necessary
                    }
                }
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    throw new Exception(error);
                }
            }
        }
    }

    public class DriverInfo
    {
        public string Name { get; set; } = string.Empty;
        public string CurrentVersion { get; set; } = string.Empty;
    }
}
