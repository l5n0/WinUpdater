using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace UpdateManagerApp
{
    public partial class MainForm : MaterialForm
    {
        private MaterialButton btnUpdateAllApplications;
        private MaterialButton btnUpdateSpecificApplication;
        private MaterialButton btnUpdateWindowsServicesAndDrivers;
        private MaterialButton btnUpdateDeviceDrivers;

        public MainForm()
        {
            btnUpdateAllApplications = new MaterialButton();
            btnUpdateSpecificApplication = new MaterialButton();
            btnUpdateWindowsServicesAndDrivers = new MaterialButton();
            btnUpdateDeviceDrivers = new MaterialButton();

            InitializeComponent();
            InitializeMaterialSkin();
        }

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void btnUpdateAllApplications_Click(object sender, EventArgs e)
        {
            ProgressForm progressForm = new ProgressForm();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s, args) => ExecutePowerShellScript("PowerShellScripts/UpdateAllApplications.ps1", worker);
            worker.ProgressChanged += (s, args) =>
            {
                progressForm.ProgressBar.Value = args.ProgressPercentage;
                progressForm.StatusLabel.Text = args.UserState?.ToString();
            };
            worker.RunWorkerCompleted += (s, args) => progressForm.Close();
            worker.RunWorkerAsync();

            progressForm.ShowDialog();
        }

        private void btnUpdateSpecificApplication_Click(object sender, EventArgs e)
        {
            List<ApplicationInfo> allApplications = GetInstalledApplications();
            List<ApplicationInfo> updatableApplications = GetUpgradableApplications();

            ShowApplicationSelectionDialog(allApplications, updatableApplications);
        }

        private void btnUpdateWindowsServicesAndDrivers_Click(object sender, EventArgs e)
        {
            ProgressForm progressForm = new ProgressForm();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s, args) => ExecutePowerShellScript("PowerShellScripts/UpdateWindowsServicesAndDrivers.ps1", worker);
            worker.ProgressChanged += (s, args) =>
            {
                progressForm.ProgressBar.Value = args.ProgressPercentage;
                progressForm.StatusLabel.Text = args.UserState?.ToString();
            };
            worker.RunWorkerCompleted += (s, args) => progressForm.Close();
            worker.RunWorkerAsync();

            progressForm.ShowDialog();
        }

        private void btnUpdateDeviceDrivers_Click(object sender, EventArgs e)
        {
            ProgressForm progressForm = new ProgressForm();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += (s, args) => ExecutePowerShellScript("PowerShellScripts/UpdateDeviceDrivers.ps1", worker);
            worker.ProgressChanged += (s, args) =>
            {
                progressForm.ProgressBar.Value = args.ProgressPercentage;
                progressForm.StatusLabel.Text = args.UserState?.ToString();
            };
            worker.RunWorkerCompleted += (s, args) => progressForm.Close();
            worker.RunWorkerAsync();

            progressForm.ShowDialog();
        }

        private void ExecutePowerShellScript(string scriptPath, BackgroundWorker worker, string arguments = "")
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

        private List<ApplicationInfo> GetInstalledApplications()
        {
            List<ApplicationInfo> applications = new List<ApplicationInfo>();
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Get-StartApps | Select-Object Name\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process() { StartInfo = startInfo })
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

        private List<ApplicationInfo> GetUpgradableApplications()
        {
            List<ApplicationInfo> applications = new List<ApplicationInfo>();
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"winget upgrade\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process() { StartInfo = startInfo })
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

        private void ShowApplicationSelectionDialog(List<ApplicationInfo> allApplications, List<ApplicationInfo> updatableApplications)
        {
            using (var dialog = new Form())
            {
                dialog.Text = "Select Application to Update";
                dialog.Size = new System.Drawing.Size(600, 400);

                var allAppsGridView = new DataGridView
                {
                    DataSource = allApplications,
                    Dock = DockStyle.Top,
                    Height = 150
                };

                var updatesGridView = new DataGridView
                {
                    DataSource = updatableApplications,
                    Dock = DockStyle.Bottom,
                    Height = 150
                };

                var updateButton = new MaterialButton
                {
                    Text = "Update Selected Application",
                    Dock = DockStyle.Fill
                };

                updateButton.Click += (s, e) =>
                {
                    if (updatesGridView.SelectedRows.Count > 0)
                    {
                        var selectedApp = updatesGridView.SelectedRows[0].DataBoundItem as ApplicationInfo;
                        if (selectedApp != null)
                        {
                            ProgressForm progressForm = new ProgressForm();
                            BackgroundWorker worker = new BackgroundWorker();
                            worker.WorkerReportsProgress = true;
                            worker.DoWork += (sender, args) => ExecutePowerShellScript("PowerShellScripts/UpdateSpecificApplication.ps1", worker, $"\"{selectedApp.Name}\"");
                            worker.ProgressChanged += (sender, args) =>
                            {
                                progressForm.ProgressBar.Value = args.ProgressPercentage;
                                progressForm.StatusLabel.Text = args.UserState?.ToString();
                            };
                            worker.RunWorkerCompleted += (sender, args) => progressForm.Close();
                            worker.RunWorkerAsync();

                            progressForm.ShowDialog();
                        }
                    }
                };

                dialog.Controls.Add(allAppsGridView);
                dialog.Controls.Add(updatesGridView);
                dialog.Controls.Add(updateButton);

                dialog.ShowDialog();
            }
        }
    }
}