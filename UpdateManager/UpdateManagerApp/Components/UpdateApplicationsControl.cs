using System;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class UpdateApplicationsControl : UserControl
    {
        public UpdateApplicationsControl()
        {
            InitializeComponent();
            LoadApplications();
        }

        private void InitializeComponent()
        {
            this.updateAllButton = new Button();
            this.updateSpecificButton = new Button();
            this.applicationsGridView = new DataGridView();

            // Update All Button
            this.updateAllButton.Text = "Update All Applications";
            this.updateAllButton.Dock = DockStyle.Top;
            this.updateAllButton.Click += UpdateAllButton_Click;

            // Update Specific Button
            this.updateSpecificButton.Text = "Update Specific Application";
            this.updateSpecificButton.Dock = DockStyle.Top;
            this.updateSpecificButton.Click += UpdateSpecificButton_Click;

            // Applications GridView
            this.applicationsGridView.Dock = DockStyle.Fill;
            this.applicationsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.applicationsGridView.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            // Layout
            this.Controls.Add(this.applicationsGridView);
            this.Controls.Add(this.updateSpecificButton);
            this.Controls.Add(this.updateAllButton);
        }

        private void LoadApplications()
        {
            try
            {
                var applications = UpdateActions.GetInstalledApplications();
                this.applicationsGridView.DataSource = applications;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading applications: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateAllButton_Click(object sender, EventArgs e)
        {
            UpdateActions.UpdateAllApplications();
        }

        private void UpdateSpecificButton_Click(object sender, EventArgs e)
        {
            if (this.applicationsGridView.SelectedRows.Count > 0)
            {
                var selectedApp = (ApplicationInfo)this.applicationsGridView.SelectedRows[0].DataBoundItem;
                UpdateActions.UpdateSpecificApplication(selectedApp.Name);
            }
        }

        private Button updateAllButton;
        private Button updateSpecificButton;
        private DataGridView applicationsGridView;
    }
}
