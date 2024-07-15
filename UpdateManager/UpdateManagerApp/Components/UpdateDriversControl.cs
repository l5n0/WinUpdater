using System;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class UpdateDriversControl : UserControl
    {
        public UpdateDriversControl()
        {
            InitializeComponent();
            LoadDrivers();
        }

        private void InitializeComponent()
        {
            this.updateAllDriversButton = new Button();
            this.updateSpecificDriversButton = new Button();
            this.driversGridView = new DataGridView();

            // Update All Drivers Button
            this.updateAllDriversButton.Text = "Update All Drivers";
            this.updateAllDriversButton.Dock = DockStyle.Top;
            this.updateAllDriversButton.Click += UpdateAllDriversButton_Click;

            // Update Specific Drivers Button
            this.updateSpecificDriversButton.Text = "Update Specific Drivers";
            this.updateSpecificDriversButton.Dock = DockStyle.Top;
            this.updateSpecificDriversButton.Click += UpdateSpecificDriversButton_Click;

            // Drivers GridView
            this.driversGridView.Dock = DockStyle.Fill;
            this.driversGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.driversGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            // Layout
            this.Controls.Add(this.driversGridView);
            this.Controls.Add(this.updateSpecificDriversButton);
            this.Controls.Add(this.updateAllDriversButton);
        }

        private void LoadDrivers()
        {
            // Load the list of drivers
            var drivers = UpdateActions.GetInstalledDrivers();
            this.driversGridView.DataSource = drivers;
        }

        private void UpdateAllDriversButton_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateAllDrivers();
        }

        private void UpdateSpecificDriversButton_Click(object sender, EventArgs e)
        {
            if (this.driversGridView.SelectedRows.Count > 0)
            {
                var selectedDriver = (DriverInfo)this.driversGridView.SelectedRows[0].DataBoundItem;
                UpdateActions.UpdateSpecificDriver(selectedDriver.Name);
            }
        }

        private Button updateAllDriversButton;
        private Button updateSpecificDriversButton;
        private DataGridView driversGridView;
    }
}
