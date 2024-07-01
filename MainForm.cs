using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UpdateManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnUpdateAllApplications_Click(object sender, EventArgs e)
        {
            ExecutePowerShellScript("UpdateAllApplications.ps1");
        }

        private void btnUpdateSpecificApplication_Click(object sender, EventArgs e)
        {
            string appName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the application you want to update", "Update Specific Application", "");
            if (!string.IsNullOrWhiteSpace(appName))
            {
                ExecutePowerShellScript("UpdateSpecificApplication.ps1", $"-appName \"{appName}\"");
            }
        }

        private void btnUpdateWindowsServicesAndDrivers_Click(object sender, EventArgs e)
        {
            ExecutePowerShellScript("UpdateWindowsServicesAndDrivers.ps1");
        }

        private void btnUpdateDeviceDrivers_Click(object sender, EventArgs e)
        {
            ExecutePowerShellScript("UpdateDeviceDrivers.ps1");
        }

        private void ExecutePowerShellScript(string scriptPath, string arguments = "")
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
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(output))
                {
                    MessageBox.Show(output, "Output", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
