using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UpdateManagerApp
{
    public static class UpdateActions
    {
        public static void UpdateAllApplications()
        {
            ExecuteUpdate("PowerShellScripts/UpdateAllApplications.ps1");
        }

        public static void UpdateSpecificApplication(string appName)
        {
            ExecuteUpdate("PowerShellScripts/UpdateSpecificApplication.ps1", appName);
        }

        public static void UpdateWindowsServicesAndDrivers()
        {
            ExecuteUpdate("PowerShellScripts/UpdateWindowsServicesAndDrivers.ps1");
        }

        public static void UpdateAllDrivers()
        {
            ExecuteUpdate("PowerShellScripts/UpdateAllDrivers.ps1");
        }

        public static void UpdateSpecificDriver(string driverName)
        {
            ExecuteUpdate("PowerShellScripts/UpdateSpecificDriver.ps1", driverName);
        }

        public static List<ApplicationInfo> GetInstalledApplications()
        {
            var applications = new List<ApplicationInfo>();
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PowerShellScripts", "GetInstalledApplications.ps1");

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

            // Remove duplicate entries based on Name
            var uniqueApplications = new Dictionary<string, ApplicationInfo>();
            foreach (var app in applications)
            {
                if (!uniqueApplications.ContainsKey(app.Name))
                {
                    uniqueApplications.Add(app.Name, app);
                }
            }

            return new List<ApplicationInfo>(uniqueApplications.Values);
        }

        public static List<DriverInfo> GetInstalledDrivers()
        {
            var drivers = new List<DriverInfo>();
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PowerShellScripts", "GetInstalledDrivers.ps1");

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
                                CurrentVersion = columns[1],
                                IsUpToDate = true // Assuming you have logic to determine if up-to-date
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

            // Remove duplicate entries based on Name
            var uniqueDrivers = new Dictionary<string, DriverInfo>();
            foreach (var driver in drivers)
            {
                if (!uniqueDrivers.ContainsKey(driver.Name))
                {
                    uniqueDrivers.Add(driver.Name, driver);
                }
            }

            return new List<DriverInfo>(uniqueDrivers.Values);
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
        public bool IsUpToDate { get; set; }
    }
}
