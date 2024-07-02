using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class MainForm : MaterialForm
    {
        private void InitializeComponent()
        {
            this.btnUpdateAllApplications = new MaterialButton();
            this.btnUpdateSpecificApplication = new MaterialButton();
            this.btnUpdateWindowsServicesAndDrivers = new MaterialButton();
            this.btnUpdateDeviceDrivers = new MaterialButton();

            this.SuspendLayout();

            // 
            // btnUpdateAllApplications
            // 
            this.btnUpdateAllApplications.Location = new System.Drawing.Point(50, 80);
            this.btnUpdateAllApplications.Name = "btnUpdateAllApplications";
            this.btnUpdateAllApplications.Size = new System.Drawing.Size(400, 60);
            this.btnUpdateAllApplications.TabIndex = 0;
            this.btnUpdateAllApplications.Text = "Update All Applications";
            this.btnUpdateAllApplications.UseVisualStyleBackColor = true;
            this.btnUpdateAllApplications.Click += new System.EventHandler(this.btnUpdateAllApplications_Click);

            // 
            // btnUpdateSpecificApplication
            // 
            this.btnUpdateSpecificApplication.Location = new System.Drawing.Point(50, 150);
            this.btnUpdateSpecificApplication.Name = "btnUpdateSpecificApplication";
            this.btnUpdateSpecificApplication.Size = new System.Drawing.Size(400, 60);
            this.btnUpdateSpecificApplication.TabIndex = 1;
            this.btnUpdateSpecificApplication.Text = "Update Specific Application";
            this.btnUpdateSpecificApplication.UseVisualStyleBackColor = true;
            this.btnUpdateSpecificApplication.Click += new System.EventHandler(this.btnUpdateSpecificApplication_Click);

            // 
            // btnUpdateWindowsServicesAndDrivers
            // 
            this.btnUpdateWindowsServicesAndDrivers.Location = new System.Drawing.Point(50, 220);
            this.btnUpdateWindowsServicesAndDrivers.Name = "btnUpdateWindowsServicesAndDrivers";
            this.btnUpdateWindowsServicesAndDrivers.Size = new System.Drawing.Size(400, 60);
            this.btnUpdateWindowsServicesAndDrivers.TabIndex = 2;
            this.btnUpdateWindowsServicesAndDrivers.Text = "Update Windows Services and Drivers";
            this.btnUpdateWindowsServicesAndDrivers.UseVisualStyleBackColor = true;
            this.btnUpdateWindowsServicesAndDrivers.Click += new System.EventHandler(this.btnUpdateWindowsServicesAndDrivers_Click);

            // 
            // btnUpdateDeviceDrivers
            // 
            this.btnUpdateDeviceDrivers.Location = new System.Drawing.Point(50, 290);
            this.btnUpdateDeviceDrivers.Name = "btnUpdateDeviceDrivers";
            this.btnUpdateDeviceDrivers.Size = new System.Drawing.Size(400, 60);
            this.btnUpdateDeviceDrivers.TabIndex = 3;
            this.btnUpdateDeviceDrivers.Text = "Update Device Drivers";
            this.btnUpdateDeviceDrivers.UseVisualStyleBackColor = true;
            this.btnUpdateDeviceDrivers.Click += new System.EventHandler(this.btnUpdateDeviceDrivers_Click);

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 400);
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
