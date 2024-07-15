using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                        var columns = line.Split(new[] { ' ' }, 5, StringSplitOptions.RemoveEmptyEntries);
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

            // Ensure unique applications by name, keeping the latest version
            var uniqueApplications = applications
                .GroupBy(app => app.Name)
                .Select(group => group.OrderByDescending(app => app.CurrentVersion).First())
                .ToList();

            return uniqueApplications;
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
                        var columns = line.Split(new[] { ' ' }, 5, StringSplitOptions.RemoveEmptyEntries);
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

            // Ensure unique drivers by name, keeping the latest version
            var uniqueDrivers = drivers
                .GroupBy(driver => driver.Name)
                .Select(group => group.OrderByDescending(driver => driver.CurrentVersion).First())
                .ToList();

            return uniqueDrivers;
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
