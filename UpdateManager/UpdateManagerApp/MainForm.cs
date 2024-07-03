using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
            MaterialSkinManagerSetup.InitializeMaterialSkin(this);
        }

        private void btnUpdateAllApplications_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateAllApplications();
        }

        private void btnUpdateSpecificApplication_Click(object sender, EventArgs e)
        {
            ShowSpecificApplicationUpdatePanel();
        }

        private void btnUpdateWindowsServicesAndDrivers_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateWindowsServicesAndDrivers();
        }

        private void btnUpdateDeviceDrivers_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateDeviceDrivers();
        }

        private void ShowSpecificApplicationUpdatePanel()
        {
            // Clear existing content
            contentPanel.Controls.Clear();

            // Create "Update All Applications" button
            Button updateAllButton = new Button
            {
                Text = "Update All Applications",
                Dock = DockStyle.Top
            };
            updateAllButton.Click += btnUpdateAllApplications_Click;

            // Get lists of applications
            List<ApplicationInfo> allApplications = UpdateActions.GetInstalledApplications();
            List<ApplicationInfo> updatableApplications = UpdateActions.GetUpgradableApplications();

            // Create DataGridView for all applications
            DataGridView allAppsGridView = new DataGridView
            {
                DataSource = allApplications,
                Dock = DockStyle.Top,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DefaultCellStyle = { ForeColor = System.Drawing.Color.Black }
            };

            // Create DataGridView for updatable applications
            DataGridView updatesGridView = new DataGridView
            {
                DataSource = updatableApplications,
                Dock = DockStyle.Top,
                Height = 200,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DefaultCellStyle = { ForeColor = System.Drawing.Color.Black }
            };

            // Create update button for selected application
            Button updateButton = new Button
            {
                Text = "Update Selected Application",
                Dock = DockStyle.Top
            };
            updateButton.Click += (s, e) =>
            {
                if (updatesGridView.SelectedRows.Count > 0)
                {
                    var selectedApp = updatesGridView.SelectedRows[0].DataBoundItem as ApplicationInfo;
                    if (selectedApp != null)
                    {
                        UpdateActions.UpdateSpecificApplication(selectedApp.Name);
                    }
                }
            };

            // Add controls to content panel in the order you want them to appear
            contentPanel.Controls.Add(updateButton);
            contentPanel.Controls.Add(updatesGridView);
            contentPanel.Controls.Add(allAppsGridView);
            contentPanel.Controls.Add(updateAllButton); // This is added last so it appears at the top
        }
    }
}
