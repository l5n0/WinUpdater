using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class ProgressForm : Form
    {
        private ProgressBar progressBar;
        private Label statusLabel;

        public ProgressForm()
        {
            InitializeComponent();
        }

        public ProgressBar ProgressBar => progressBar;
        public Label StatusLabel => statusLabel;

        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.BackColor = DarkModeColors.BackgroundColor;
            this.ForeColor = DarkModeColors.ForegroundColor;

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 23);
            this.progressBar.TabIndex = 0;
            this.progressBar.BackColor = DarkModeColors.ControlColor;

            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 9);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(60, 13);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Initializing...";

            // 
            // ProgressForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 76);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.progressBar);
            this.Name = "ProgressForm";
            this.Text = "Updating Applications";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
