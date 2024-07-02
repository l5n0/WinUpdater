using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UpdateManagerApp
{
    public partial class MainForm : Form
    {
        private Button btnUpdateAllApplications;
        private Button btnUpdateSpecificApplication;
        private Button btnUpdateWindowsServicesAndDrivers;
        private Button btnUpdateDeviceDrivers;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnUpdateAllApplications_Click(object? sender, EventArgs e)
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

        private void btnUpdateSpecificApplication_Click(object? sender, EventArgs e)
        {
            List<ApplicationInfo> allApplications = GetInstalledApplications();
            List<ApplicationInfo> updatableApplications = GetUpgradableApplications();

            ShowApplicationSelectionDialog(allApplications, updatableApplications);
        }

        private void btnUpdateWindowsServicesAndDrivers_Click(object? sender, EventArgs e)
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

        private void btnUpdateDeviceDrivers_Click(object? sender, EventArgs e)
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
            Form dialog = new Form();
            dialog.Width = 800;
            dialog.Height = 600;
            dialog.Text = "Select an Application";
            dialog.BackColor = DarkModeColors.BackgroundColor;
            dialog.ForeColor = DarkModeColors.ForegroundColor;

            DataGridView allAppsGridView = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 250,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = allApplications,
                BackgroundColor = DarkModeColors.ControlColor,
                ForeColor = DarkModeColors.ForegroundColor,
                GridColor = DarkModeColors.ForegroundColor,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = DarkModeColors.ControlColor, ForeColor = DarkModeColors.ForegroundColor },
                DefaultCellStyle = new DataGridViewCellStyle { BackColor = DarkModeColors.ControlColor, ForeColor = DarkModeColors.ForegroundColor }
            };

            DataGridView updatesGridView = new DataGridView
            {
                Dock = DockStyle.Bottom,
                Height = 250,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = updatableApplications,
                BackgroundColor = DarkModeColors.ControlColor,
                ForeColor = DarkModeColors.ForegroundColor,
                GridColor = DarkModeColors.ForegroundColor,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = DarkModeColors.ControlColor, ForeColor = DarkModeColors.ForegroundColor },
                DefaultCellStyle = new DataGridViewCellStyle { BackColor = DarkModeColors.ControlColor, ForeColor = DarkModeColors.ForegroundColor }
            };

            Button updateButton = new Button
            {
                Text = "Update Selected Application",
                Dock = DockStyle.Bottom,
                Height = 30,
                DialogResult = DialogResult.OK,
                BackColor = DarkModeColors.ButtonColor,
                ForeColor = DarkModeColors.ForegroundColor
            };

            updateButton.Click += (s, e) =>
            {
                if (allAppsGridView.SelectedRows.Count > 0)
                {
                    ApplicationInfo selectedApp = allAppsGridView.SelectedRows[0].DataBoundItem as ApplicationInfo;
                    if (selectedApp != null)
                    {
                        ProgressForm progressForm = new ProgressForm();
                        BackgroundWorker worker = new BackgroundWorker();
                        worker.WorkerReportsProgress = true;
                        worker.DoWork += (s, args) => ExecutePowerShellScript("PowerShellScripts/UpdateSpecificApplication.ps1", worker, $"-appName \"{selectedApp.Name}\"");
                        worker.ProgressChanged += (s, args) =>
                        {
                            progressForm.ProgressBar.Value = args.ProgressPercentage;
                            progressForm.StatusLabel.Text = args.UserState?.ToString();
                        };
                        worker.RunWorkerCompleted += (s, args) => progressForm.Close();
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
