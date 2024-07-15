using System;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnUpdateApplications_Click(object sender, EventArgs e)
        {
            LoadControl(new UpdateApplicationsControl());
        }

        private void BtnUpdateDrivers_Click(object sender, EventArgs e)
        {
            LoadControl(new UpdateDriversControl());
        }

        private void BtnDiskUsage_Click(object sender, EventArgs e)
        {
            LoadControl(new DiskUsageControl());
        }

        private void LoadControl(UserControl control)
        {
            this.contentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            this.contentPanel.Controls.Add(control);
        }
    }
}
