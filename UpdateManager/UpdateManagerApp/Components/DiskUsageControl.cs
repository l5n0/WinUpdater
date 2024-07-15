using System;
using System.IO;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class DiskUsageControl : UserControl
    {
        public DiskUsageControl()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.diskComboBox = new ComboBox();
            this.scanButton = new Button();
            this.diskUsageProgressBar = new ProgressBar();
            this.diskUsageLabel = new Label();

            // Disk ComboBox
            this.diskComboBox.Dock = DockStyle.Top;

            // Scan Button
            this.scanButton.Text = "Scan Disk";
            this.scanButton.Dock = DockStyle.Top;
            this.scanButton.Click += ScanButton_Click;

            // Disk Usage ProgressBar
            this.diskUsageProgressBar.Dock = DockStyle.Top;

            // Disk Usage Label
            this.diskUsageLabel.Dock = DockStyle.Top;

            // Layout
            this.Controls.Add(this.diskUsageLabel);
            this.Controls.Add(this.diskUsageProgressBar);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.diskComboBox);

            LoadDrives();
        }

        private void LoadDrives()
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    this.diskComboBox.Items.Add(drive.Name);
                }
            }
        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            if (this.diskComboBox.SelectedItem != null)
            {
                var driveName = this.diskComboBox.SelectedItem.ToString();
                var drive = new DriveInfo(driveName);
                if (drive.IsReady)
                {
                    var usedSpace = drive.TotalSize - drive.AvailableFreeSpace;
                    var usedPercentage = (double)usedSpace / drive.TotalSize * 100;
                    this.diskUsageProgressBar.Value = (int)usedPercentage;
                    this.diskUsageLabel.Text = $"Used: {usedPercentage:F2}%";
                }
            }
        }

        private ComboBox diskComboBox;
        private Button scanButton;
        private ProgressBar diskUsageProgressBar;
        private Label diskUsageLabel;
    }
}
