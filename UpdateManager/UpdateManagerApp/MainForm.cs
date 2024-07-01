using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnUpdateAllApplications = new System.Windows.Forms.Button();
            this.btnUpdateSpecificApplication = new System.Windows.Forms.Button();
            this.btnUpdateWindowsServicesAndDrivers = new System.Windows.Forms.Button();
            this.btnUpdateDeviceDrivers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUpdateAllApplications
            // 
            this.btnUpdateAllApplications.Location = new System.Drawing.Point(50, 30);
            this.btnUpdateAllApplications.Name = "btnUpdateAllApplications";
            this.btnUpdateAllApplications.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateAllApplications.TabIndex = 0;
            this.btnUpdateAllApplications.Text = "Update All Applications";
            this.btnUpdateAllApplications.UseVisualStyleBackColor = true;
            this.btnUpdateAllApplications.Click += new System.EventHandler(this.btnUpdateAllApplications_Click);
            // 
            // btnUpdateSpecificApplication
            // 
            this.btnUpdateSpecificApplication.Location = new System.Drawing.Point(50, 80);
            this.btnUpdateSpecificApplication.Name = "btnUpdateSpecificApplication";
            this.btnUpdateSpecificApplication.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateSpecificApplication.TabIndex = 1;
            this.btnUpdateSpecificApplication.Text = "Update Specific Application";
            this.btnUpdateSpecificApplication.UseVisualStyleBackColor = true;
            this.btnUpdateSpecificApplication.Click += new System.EventHandler(this.btnUpdateSpecificApplication_Click);
            // 
            // btnUpdateWindowsServicesAndDrivers
            // 
            this.btnUpdateWindowsServicesAndDrivers.Location = new System.Drawing.Point(50, 130);
            this.btnUpdateWindowsServicesAndDrivers.Name = "btnUpdateWindowsServicesAndDrivers";
            this.btnUpdateWindowsServicesAndDrivers.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateWindowsServicesAndDrivers.TabIndex = 2;
            this.btnUpdateWindowsServicesAndDrivers.Text = "Update Windows Services and Drivers";
            this.btnUpdateWindowsServicesAndDrivers.UseVisualStyleBackColor = true;
            this.btnUpdateWindowsServicesAndDrivers.Click += new System.EventHandler(this.btnUpdateWindowsServicesAndDrivers_Click);
            // 
            // btnUpdateDeviceDrivers
            // 
            this.btnUpdateDeviceDrivers.Location = new System.Drawing.Point(50, 180);
            this.btnUpdateDeviceDrivers.Name = "btnUpdateDeviceDrivers";
            this.btnUpdateDeviceDrivers.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateDeviceDrivers.TabIndex = 3;
            this.btnUpdateDeviceDrivers.Text = "Update Device Drivers";
            this.btnUpdateDeviceDrivers.UseVisualStyleBackColor = true;
            this.btnUpdateDeviceDrivers.Click += new System.EventHandler(this.btnUpdateDeviceDrivers_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Controls.Add(this.btnUpdateDeviceDrivers);
            this.Controls.Add(this.btnUpdateWindowsServicesAndDrivers);
            this.Controls.Add(this.btnUpdateSpecificApplication);
            this.Controls.Add(this.btnUpdateAllApplications);
            this.Name = "MainForm";
            this.Text = "Update Manager";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnUpdateAllApplications;
        private System.Windows.Forms.Button btnUpdateSpecificApplication;
        private System.Windows.Forms.Button btnUpdateWindowsServicesAndDrivers;
        private System.Windows.Forms.Button btnUpdateDeviceDrivers;

        private void btnUpdateAllApplications_Click(object sender, EventArgs e)
        {
            ExecutePowerShellScript("PowerShellScripts/UpdateAllApplications.ps1");
        }

        private void btnUpdateSpecificApplication_Click(object sender, EventArgs e)
        {
            string appName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the application you want to update", "Update Specific Application", "");
            if (!string.IsNullOrWhiteSpace(appName))
            {
                ExecutePowerShellScript("PowerShellScripts/UpdateSpecificApplication.ps1", $"-appName \"{appName}\"");
            }
        }

        private void btnUpdateWindowsServicesAndDrivers_Click(object sender, EventArgs e)
        {
            ExecutePowerShellScript("PowerShellScripts/UpdateWindowsServicesAndDrivers.ps1");
        }

        private void btnUpdateDeviceDrivers_Click(object sender, EventArgs e)
        {
            ExecutePowerShellScript("PowerShellScripts/UpdateDeviceDrivers.ps1");
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
