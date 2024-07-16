namespace UpdateManagerApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnUpdateApplications;
        private System.Windows.Forms.Button btnUpdateDrivers;
        private System.Windows.Forms.Button btnDiskUsage;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnUpdateApplications = new System.Windows.Forms.Button();
            this.btnUpdateDrivers = new System.Windows.Forms.Button();
            this.btnDiskUsage = new System.Windows.Forms.Button();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.Controls.Add(this.btnHome);
            this.sidebarPanel.Controls.Add(this.btnUpdateApplications);
            this.sidebarPanel.Controls.Add(this.btnUpdateDrivers);
            this.sidebarPanel.Controls.Add(this.btnDiskUsage);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(200, 600);
            this.sidebarPanel.TabIndex = 0;
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(200, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(600, 600);
            this.contentPanel.TabIndex = 1;
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(200, 50);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // btnUpdateApplications
            // 
            this.btnUpdateApplications.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUpdateApplications.Location = new System.Drawing.Point(0, 50);
            this.btnUpdateApplications.Name = "btnUpdateApplications";
            this.btnUpdateApplications.Size = new System.Drawing.Size(200, 50);
            this.btnUpdateApplications.TabIndex = 1;
            this.btnUpdateApplications.Text = "Update Applications";
            this.btnUpdateApplications.UseVisualStyleBackColor = true;
            this.btnUpdateApplications.Click += new System.EventHandler(this.BtnUpdateApplications_Click);
            // 
            // btnUpdateDrivers
            // 
            this.btnUpdateDrivers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUpdateDrivers.Location = new System.Drawing.Point(0, 100);
            this.btnUpdateDrivers.Name = "btnUpdateDrivers";
            this.btnUpdateDrivers.Size = new System.Drawing.Size(200, 50);
            this.btnUpdateDrivers.TabIndex = 2;
            this.btnUpdateDrivers.Text = "Update Drivers";
            this.btnUpdateDrivers.UseVisualStyleBackColor = true;
            this.btnUpdateDrivers.Click += new System.EventHandler(this.BtnUpdateDrivers_Click);
            // 
            // btnDiskUsage
            // 
            this.btnDiskUsage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDiskUsage.Location = new System.Drawing.Point(0, 150);
            this.btnDiskUsage.Name = "btnDiskUsage";
            this.btnDiskUsage.Size = new System.Drawing.Size(200, 50);
            this.btnDiskUsage.TabIndex = 3;
            this.btnDiskUsage.Text = "Disk Usage";
            this.btnDiskUsage.UseVisualStyleBackColor = true;
            this.btnDiskUsage.Click += new System.EventHandler(this.BtnDiskUsage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "MainForm";
            this.Text = "Update Manager";
            this.sidebarPanel.ResumeLayout(false);
            this.ResumeLayout(false);

            // Add the btnHome and its event handler
            this.btnHome = new System.Windows.Forms.Button();
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(200, 50);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.BtnHome_Click);

        }

        #endregion
    }
}
