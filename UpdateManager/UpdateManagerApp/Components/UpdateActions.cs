using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin.Controls;

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

        public static void UpdateDeviceDrivers()
        {
            ExecuteUpdate("PowerShellScripts/UpdateDeviceDrivers.ps1");
        }

        private static void ExecuteUpdate(string scriptPath, string arguments = "")
        {
            ProgressForm progressForm = new ProgressForm();
            BackgroundWorker worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            worker.DoWork += (s, args) => ExecutePowerShellScript(scriptPath, worker, arguments);
            worker.ProgressChanged += (s, args) =>
            {
                progressForm.ProgressBar.Value = args.ProgressPercentage;
                progressForm.StatusLabel.Text = args.UserState?.ToString();
            };
            worker.RunWorkerCompleted += (s, args) => progressForm.Close();
            worker.RunWorkerAsync();
            progressForm.ShowDialog();
        }

        private static void ExecutePowerShellScript(string scriptPath, BackgroundWorker worker, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\" {arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (line != null)
                    {
                        worker.ReportProgress(0, line); // This can be improved by parsing progress percentage if available
                    }
                }
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static List<ApplicationInfo> GetInstalledApplications()
        {
            List<ApplicationInfo> applications = new List<ApplicationInfo>();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Get-StartApps | Select-Object Name\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines.Skip(3)) // Skip header
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            applications.Add(new ApplicationInfo
                            {
                                Name = line.Trim(),
                                CurrentVersion = "Unknown",
                                AvailableVersion = ""
                            });
                        }
                    }
                }
            }
            return applications;
        }

        public static List<ApplicationInfo> GetUpgradableApplications()
        {
            List<ApplicationInfo> applications = new List<ApplicationInfo>();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"winget upgrade\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    bool foundHeader = false;
                    foreach (string line in lines)
                    {
                        if (foundHeader)
                        {
                            var columns = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            if (columns.Count >= 5)
                            {
                                string name = columns[0];
                                string currentVersion = columns[2];
                                string availableVersion = columns[3];
                                applications.Add(new ApplicationInfo
                                {
                                    Name = name,
                                    CurrentVersion = currentVersion,
                                    AvailableVersion = availableVersion
                                });
                            }
                        }
                        else if (line.StartsWith("Name", StringComparison.OrdinalIgnoreCase))
                        {
                            foundHeader = true;
                        }
                    }
                }
            }
            return applications;
        }
    }
}
