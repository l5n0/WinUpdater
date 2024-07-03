using MaterialSkin.Controls;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class MainForm : MaterialForm
    {
        private TableLayoutPanel tableLayoutPanel;
        private DataGridView dataGridView;
        private MaterialButton btnUpdateAllApplications;
        private MaterialButton btnUpdateSpecificApplication;
        private MaterialButton btnUpdateWindowsServicesAndDrivers;
        private MaterialButton btnUpdateDeviceDrivers;

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new TableLayoutPanel();
            this.dataGridView = new DataGridView();
            this.btnUpdateAllApplications = new MaterialButton();
            this.btnUpdateSpecificApplication = new MaterialButton();
            this.btnUpdateWindowsServicesAndDrivers = new MaterialButton();
            this.btnUpdateDeviceDrivers = new MaterialButton();

            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel.Dock = DockStyle.Fill;
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // 
            // btnUpdateAllApplications
            // 
            this.btnUpdateAllApplications.Dock = DockStyle.Fill;
            this.btnUpdateAllApplications.Text = "Update All Applications";
            this.btnUpdateAllApplications.Click += new EventHandler(this.btnUpdateAllApplications_Click);
            this.tableLayoutPanel.Controls.Add(this.btnUpdateAllApplications, 0, 0);

            // 
            // btnUpdateSpecificApplication
            // 
            this.btnUpdateSpecificApplication.Dock = DockStyle.Fill;
            this.btnUpdateSpecificApplication.Text = "Update Specific Application";
            this.btnUpdateSpecificApplication.Click += new EventHandler(this.btnUpdateSpecificApplication_Click);
            this.tableLayoutPanel.Controls.Add(this.btnUpdateSpecificApplication, 0, 1);

            // 
            // btnUpdateWindowsServicesAndDrivers
            // 
            this.btnUpdateWindowsServicesAndDrivers.Dock = DockStyle.Fill;
            this.btnUpdateWindowsServicesAndDrivers.Text = "Update Windows Services and Drivers";
            this.btnUpdateWindowsServicesAndDrivers.Click += new EventHandler(this.btnUpdateWindowsServicesAndDrivers_Click);
            this.tableLayoutPanel.Controls.Add(this.btnUpdateWindowsServicesAndDrivers, 0, 2);

            // 
            // btnUpdateDeviceDrivers
            // 
            this.btnUpdateDeviceDrivers.Dock = DockStyle.Fill;
            this.btnUpdateDeviceDrivers.Text = "Update Device Drivers";
            this.btnUpdateDeviceDrivers.Click += new EventHandler(this.btnUpdateDeviceDrivers_Click);
            this.tableLayoutPanel.Controls.Add(this.btnUpdateDeviceDrivers, 0, 3);

            // 
            // dataGridView
            // 
            this.dataGridView.Dock = DockStyle.Fill;
            this.tableLayoutPanel.Controls.Add(this.dataGridView, 1, 0);
            this.tableLayoutPanel.SetRowSpan(this.dataGridView, 4);

            // 
            // MainForm
            // 
            this.Padding = new Padding(10, 50, 10, 10); // Add padding to ensure controls don't interfere with the title bar
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "Update Manager";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void AddButtonToTable(string text, EventHandler clickHandler, int rowIndex)
        {
            var button = new MaterialButton
            {
                Text = text,
                Dock = DockStyle.Fill
            };
            button.Click += clickHandler;
            this.tableLayoutPanel.Controls.Add(button, 0, rowIndex);
        }
    }
}
