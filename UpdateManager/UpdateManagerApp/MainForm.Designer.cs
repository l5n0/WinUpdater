namespace UpdateManagerApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.DataGridView dataGridView;
        private MaterialSkin.Controls.MaterialButton btnUpdateSpecificApplication;
        private MaterialSkin.Controls.MaterialButton btnUpdateWindowsServicesAndDrivers;
        private MaterialSkin.Controls.MaterialButton btnUpdateDeviceDrivers;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel = new TableLayoutPanel();
            btnUpdateSpecificApplication = new MaterialSkin.Controls.MaterialButton();
            btnUpdateWindowsServicesAndDrivers = new MaterialSkin.Controls.MaterialButton();
            btnUpdateDeviceDrivers = new MaterialSkin.Controls.MaterialButton();
            contentPanel = new Panel();
            dataGridView = new DataGridView();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(btnUpdateSpecificApplication, 0, 1);
            tableLayoutPanel.Controls.Add(btnUpdateWindowsServicesAndDrivers, 0, 2);
            tableLayoutPanel.Controls.Add(btnUpdateDeviceDrivers, 0, 3);
            tableLayoutPanel.Controls.Add(contentPanel, 1, 1);
            tableLayoutPanel.Location = new Point(3, 64);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel.Size = new Size(794, 533);
            tableLayoutPanel.TabIndex = 0;
            // 
            // btnUpdateSpecificApplication
            // 
            btnUpdateSpecificApplication.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnUpdateSpecificApplication.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnUpdateSpecificApplication.Depth = 0;
            btnUpdateSpecificApplication.Dock = DockStyle.Fill;
            btnUpdateSpecificApplication.HighEmphasis = true;
            btnUpdateSpecificApplication.Icon = null;
            btnUpdateSpecificApplication.Location = new Point(4, 56);
            btnUpdateSpecificApplication.Margin = new Padding(4, 6, 4, 6);
            btnUpdateSpecificApplication.MouseState = MaterialSkin.MouseState.HOVER;
            btnUpdateSpecificApplication.Name = "btnUpdateSpecificApplication";
            btnUpdateSpecificApplication.Size = new Size(192, 38);
            btnUpdateSpecificApplication.TabIndex = 1;
            btnUpdateSpecificApplication.Text = "Update Specific Application";
            btnUpdateSpecificApplication.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnUpdateSpecificApplication.UseAccentColor = false;
            btnUpdateSpecificApplication.Click += btnUpdateSpecificApplication_Click;
            // 
            // btnUpdateWindowsServicesAndDrivers
            // 
            btnUpdateWindowsServicesAndDrivers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnUpdateWindowsServicesAndDrivers.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnUpdateWindowsServicesAndDrivers.Depth = 0;
            btnUpdateWindowsServicesAndDrivers.Dock = DockStyle.Fill;
            btnUpdateWindowsServicesAndDrivers.HighEmphasis = true;
            btnUpdateWindowsServicesAndDrivers.Icon = null;
            btnUpdateWindowsServicesAndDrivers.Location = new Point(4, 106);
            btnUpdateWindowsServicesAndDrivers.Margin = new Padding(4, 6, 4, 6);
            btnUpdateWindowsServicesAndDrivers.MouseState = MaterialSkin.MouseState.HOVER;
            btnUpdateWindowsServicesAndDrivers.Name = "btnUpdateWindowsServicesAndDrivers";
            btnUpdateWindowsServicesAndDrivers.Size = new Size(192, 38);
            btnUpdateWindowsServicesAndDrivers.TabIndex = 2;
            btnUpdateWindowsServicesAndDrivers.Text = "Update Windows Services and Drivers";
            btnUpdateWindowsServicesAndDrivers.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnUpdateWindowsServicesAndDrivers.UseAccentColor = false;
            btnUpdateWindowsServicesAndDrivers.Click += btnUpdateWindowsServicesAndDrivers_Click;
            // 
            // btnUpdateDeviceDrivers
            // 
            btnUpdateDeviceDrivers.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnUpdateDeviceDrivers.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            btnUpdateDeviceDrivers.Depth = 0;
            btnUpdateDeviceDrivers.Dock = DockStyle.Fill;
            btnUpdateDeviceDrivers.HighEmphasis = true;
            btnUpdateDeviceDrivers.Icon = null;
            btnUpdateDeviceDrivers.Location = new Point(4, 156);
            btnUpdateDeviceDrivers.Margin = new Padding(4, 6, 4, 6);
            btnUpdateDeviceDrivers.MouseState = MaterialSkin.MouseState.HOVER;
            btnUpdateDeviceDrivers.Name = "btnUpdateDeviceDrivers";
            btnUpdateDeviceDrivers.Size = new Size(192, 38);
            btnUpdateDeviceDrivers.TabIndex = 3;
            btnUpdateDeviceDrivers.Text = "Update Device Drivers";
            btnUpdateDeviceDrivers.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            btnUpdateDeviceDrivers.UseAccentColor = false;
            btnUpdateDeviceDrivers.Click += btnUpdateDeviceDrivers_Click;
            // 
            // contentPanel
            // 
            contentPanel.Dock = DockStyle.Top;
            contentPanel.Location = new Point(203, 53);
            contentPanel.Name = "contentPanel";
            tableLayoutPanel.SetRowSpan(contentPanel, 5);
            contentPanel.Size = new Size(588, 458);
            contentPanel.TabIndex = 5;
            // 
            // dataGridView
            // 
            dataGridView.Location = new Point(0, 0);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(240, 150);
            dataGridView.TabIndex = 0;
            // 
            // MainForm
            // 
            ClientSize = new Size(800, 600);
            Controls.Add(tableLayoutPanel);
            Name = "MainForm";
            Text = "Update Manager";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        private Panel contentPanel;
    }
}
