using MaterialSkin.Controls;
using System;
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
            UpdateActions.UpdateSpecificApplication();
        }

        private void btnUpdateWindowsServicesAndDrivers_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateWindowsServicesAndDrivers();
        }

        private void btnUpdateDeviceDrivers_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateDeviceDrivers();
        }
    }
}
