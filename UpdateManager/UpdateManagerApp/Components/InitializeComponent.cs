using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class MainForm : Form
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.BackColor = DarkModeColors.BackgroundColor;
            this.ForeColor = DarkModeColors.ForegroundColor;

            // 
            // btnUpdateAllApplications
            // 
            this.btnUpdateAllApplications = new System.Windows.Forms.Button();
            this.btnUpdateAllApplications.Location = new System.Drawing.Point(50, 30);
            this.btnUpdateAllApplications.Name = "btnUpdateAllApplications";
            this.btnUpdateAllApplications.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateAllApplications.TabIndex = 0;
            this.btnUpdateAllApplications.Text = "Update All Applications";
            this.btnUpdateAllApplications.UseVisualStyleBackColor = false;
            this.btnUpdateAllApplications.BackColor = DarkModeColors.ButtonColor;
            this.btnUpdateAllApplications.ForeColor = DarkModeColors.ForegroundColor;
            this.btnUpdateAllApplications.Click += new System.EventHandler(this.btnUpdateAllApplications_Click);

            // 
            // btnUpdateSpecificApplication
            // 
            this.btnUpdateSpecificApplication = new System.Windows.Forms.Button();
            this.btnUpdateSpecificApplication.Location = new System.Drawing.Point(50, 80);
            this.btnUpdateSpecificApplication.Name = "btnUpdateSpecificApplication";
            this.btnUpdateSpecificApplication.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateSpecificApplication.TabIndex = 1;
            this.btnUpdateSpecificApplication.Text = "Update Specific Application";
            this.btnUpdateSpecificApplication.UseVisualStyleBackColor = false;
            this.btnUpdateSpecificApplication.BackColor = DarkModeColors.ButtonColor;
            this.btnUpdateSpecificApplication.ForeColor = DarkModeColors.ForegroundColor;
            this.btnUpdateSpecificApplication.Click += new System.EventHandler(this.btnUpdateSpecificApplication_Click);

            // 
            // btnUpdateWindowsServicesAndDrivers
            // 
            this.btnUpdateWindowsServicesAndDrivers = new System.Windows.Forms.Button();
            this.btnUpdateWindowsServicesAndDrivers.Location = new System.Drawing.Point(50, 130);
            this.btnUpdateWindowsServicesAndDrivers.Name = "btnUpdateWindowsServicesAndDrivers";
            this.btnUpdateWindowsServicesAndDrivers.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateWindowsServicesAndDrivers.TabIndex = 2;
            this.btnUpdateWindowsServicesAndDrivers.Text = "Update Windows Services and Drivers";
            this.btnUpdateWindowsServicesAndDrivers.UseVisualStyleBackColor = false;
            this.btnUpdateWindowsServicesAndDrivers.BackColor = DarkModeColors.ButtonColor;
            this.btnUpdateWindowsServicesAndDrivers.ForeColor = DarkModeColors.ForegroundColor;
            this.btnUpdateWindowsServicesAndDrivers.Click += new System.EventHandler(this.btnUpdateWindowsServicesAndDrivers_Click);

            // 
            // btnUpdateDeviceDrivers
            // 
            this.btnUpdateDeviceDrivers = new System.Windows.Forms.Button();
            this.btnUpdateDeviceDrivers.Location = new System.Drawing.Point(50, 180);
            this.btnUpdateDeviceDrivers.Name = "btnUpdateDeviceDrivers";
            this.btnUpdateDeviceDrivers.Size = new System.Drawing.Size(300, 40);
            this.btnUpdateDeviceDrivers.TabIndex = 3;
            this.btnUpdateDeviceDrivers.Text = "Update Device Drivers";
            this.btnUpdateDeviceDrivers.UseVisualStyleBackColor = false;
            this.btnUpdateDeviceDrivers.BackColor = DarkModeColors.ButtonColor;
            this.btnUpdateDeviceDrivers.ForeColor = DarkModeColors.ForegroundColor;
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
    }
}
