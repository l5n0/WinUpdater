using System;
using System.Windows.Forms;
using LiveCharts.Wpf;
using System.Windows.Forms.Integration;
using System.Windows.Media;

namespace UpdateManagerApp
{
    public partial class HomeControl : UserControl
    {
        private Label lblApplications;
        private ProgressBar applicationsProgressBar;
        private Label lblDrivers;
        private ProgressBar driversProgressBar;
        private ElementHost elementHost;
        private PieChart pieChart;

        public HomeControl()
        {
            InitializeComponent();
            LoadChart();
        }

        private void InitializeComponent()
        {
            this.lblApplications = new Label();
            this.applicationsProgressBar = new ProgressBar();
            this.lblDrivers = new Label();
            this.driversProgressBar = new ProgressBar();
            this.elementHost = new ElementHost();
            this.pieChart = new PieChart();

            // Labels and ProgressBars setup
            this.lblApplications.Text = "Applications Up-to-date:";
            this.lblApplications.Dock = DockStyle.Top;
            this.applicationsProgressBar.Dock = DockStyle.Top;

            this.lblDrivers.Text = "Drivers Up-to-date:";
            this.lblDrivers.Dock = DockStyle.Top;
            this.driversProgressBar.Dock = DockStyle.Top;

            // ElementHost setup
            this.elementHost.Dock = DockStyle.Fill;
            this.elementHost.Child = this.pieChart;

            // Layout
            this.Controls.Add(this.elementHost);
            this.Controls.Add(this.driversProgressBar);
            this.Controls.Add(this.lblDrivers);
            this.Controls.Add(this.applicationsProgressBar);
            this.Controls.Add(this.lblApplications);
        }

        private void LoadChart()
        {
            // Simulated data
            int totalApplications = 100;
            int upToDateApplications = 75;
            int outOfDateApplications = totalApplications - upToDateApplications;

            int totalDrivers = 50;
            int upToDateDrivers = 45;
            int outOfDateDrivers = totalDrivers - upToDateDrivers;

            // Update ProgressBars
            this.applicationsProgressBar.Maximum = totalApplications;
            this.applicationsProgressBar.Value = upToDateApplications;

            this.driversProgressBar.Maximum = totalDrivers;
            this.driversProgressBar.Value = upToDateDrivers;

            // Update PieChart
            this.pieChart.Series = new LiveCharts.SeriesCollection
            {
                new PieSeries { Title = "Up-to-date Applications", Values = new LiveCharts.ChartValues<int> { upToDateApplications }, Fill = System.Windows.Media.Brushes.Green },
                new PieSeries { Title = "Out-of-date Applications", Values = new LiveCharts.ChartValues<int> { outOfDateApplications }, Fill = System.Windows.Media.Brushes.Red },
                new PieSeries { Title = "Up-to-date Drivers", Values = new LiveCharts.ChartValues<int> { upToDateDrivers }, Fill = System.Windows.Media.Brushes.Blue },
                new PieSeries { Title = "Out-of-date Drivers", Values = new LiveCharts.ChartValues<int> { outOfDateDrivers }, Fill = System.Windows.Media.Brushes.Orange }
            };
        }
    }
}
