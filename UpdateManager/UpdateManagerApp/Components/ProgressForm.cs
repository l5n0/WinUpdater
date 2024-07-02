using MaterialSkin.Controls;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class ProgressForm : MaterialForm
    {
        public ProgressForm()
        {
            InitializeComponent();
            MaterialSkinManagerSetup.InitializeMaterialSkin(this);
        }

        public MaterialProgressBar ProgressBar => progressBar;
        public MaterialLabel StatusLabel => statusLabel;

        private void InitializeComponent()
        {
            this.progressBar = new MaterialProgressBar();
            this.statusLabel = new MaterialLabel();

            this.SuspendLayout();

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(50, 80);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 60);
            this.progressBar.TabIndex = 0;

            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(50, 150);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(400, 60);
            this.statusLabel.TabIndex = 1;

            // 
            // ProgressForm
            // 
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusLabel);
            this.Name = "ProgressForm";
            this.Text = "Progress";
            this.ResumeLayout(false);
        }

        private MaterialProgressBar progressBar;
        private MaterialLabel statusLabel;
    }
}
