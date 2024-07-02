using MaterialSkin;
using MaterialSkin.Controls;
using System.Windows.Forms;

namespace UpdateManagerApp
{
    public partial class ProgressForm : MaterialForm
    {
        private ProgressBar progressBar;
        private Label statusLabel;

        public ProgressForm()
        {
            progressBar = new ProgressBar();
            statusLabel = new Label();

            InitializeComponent();
            InitializeMaterialSkin();
        }

        public ProgressBar ProgressBar => progressBar;
        public Label StatusLabel => statusLabel;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 23);
            this.progressBar.TabIndex = 0;

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

        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, Primary.BlueGrey900,
                Primary.BlueGrey500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }
    }
}