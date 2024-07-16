using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.Wpf;
using System.Windows.Forms.Integration;
using Brushes = System.Windows.Media.Brushes;

namespace UpdateManagerApp
{
    public partial class HomeControl : UserControl
    {
        private Label lblApplications = null!;
        private ProgressBar applicationsProgressBar = null!;
        private Label lblDrivers = null!;
        private ProgressBar driversProgressBar = null!;
        private ElementHost elementHostApplications = null!;
        private ElementHost elementHostDrivers = null!;
        private PieChart pieChartApplications = null!;
        private PieChart pieChartDrivers = null!;

        public HomeControl()
        {
            InitializeComponent();
            LoadChartsAsync();
        }

        private void InitializeComponent()
        {
            this.lblApplications = new Label();
            this.applicationsProgressBar = new ProgressBar();
            this.lblDrivers = new Label();
            this.driversProgressBar = new ProgressBar();
            this.elementHostApplications = new ElementHost();
            this.elementHostDrivers = new ElementHost();
            this.pieChartApplications = new PieChart();
            this.pieChartDrivers = new PieChart();

            // Labels and ProgressBars setup
            this.lblApplications.Text = "Applications Up-to-date:";
            this.lblApplications.Dock = DockStyle.Top;
            this.applicationsProgressBar.Dock = DockStyle.Top;

            this.lblDrivers.Text = "Drivers Up-to-date:";
            this.lblDrivers.Dock = DockStyle.Top;
            this.driversProgressBar.Dock = DockStyle.Top;

            // Set PieChart size
            int pieChartSize = 300;

            this.pieChartApplications.Width = pieChartSize;
            this.pieChartApplications.Height = pieChartSize;

            this.pieChartDrivers.Width = pieChartSize;
            this.pieChartDrivers.Height = pieChartSize;

            // ElementHosts setup
            this.elementHostApplications.Dock = DockStyle.Top;
            this.elementHostApplications.Child = this.pieChartApplications;
            
            this.elementHostDrivers.Dock = DockStyle.Fill;
            this.elementHostDrivers.Child = this.pieChartDrivers;

            // Layout
            this.Controls.Add(this.elementHostDrivers);
            this.Controls.Add(this.driversProgressBar);
            this.Controls.Add(this.lblDrivers);
            this.Controls.Add(this.elementHostApplications);
            this.Controls.Add(this.applicationsProgressBar);
            this.Controls.Add(this.lblApplications);
        }

        private async void LoadChartsAsync()
        {
            await Task.Run(() => LoadData());
            UpdateUI();
        }

        private (int totalApplications, int upToDateApplications, int outOfDateApplications,
                 int totalDrivers, int upToDateDrivers, int outOfDateDrivers) LoadData()
        {
            // Simulated data loading
            int totalApplications = 100;
            int upToDateApplications = 75;
            int outOfDateApplications = totalApplications - upToDateApplications;

            int totalDrivers = 50;
            int upToDateDrivers = 45;
            int outOfDateDrivers = totalDrivers - upToDateDrivers;

            return (totalApplications, upToDateApplications, outOfDateApplications,
                    totalDrivers, upToDateDrivers, outOfDateDrivers);
        }

        private void UpdateUI()
        {
            var data = LoadData();

            // Update ProgressBars
            this.applicationsProgressBar.Maximum = data.totalApplications;
            this.applicationsProgressBar.Value = data.upToDateApplications;

            this.driversProgressBar.Maximum = data.totalDrivers;
            this.driversProgressBar.Value = data.upToDateDrivers;

            // Update PieChart for Applications
            this.pieChartApplications.Series = new LiveCharts.SeriesCollection
            {
                new PieSeries { Title = "Up-to-date Applications", Values = new LiveCharts.ChartValues<int> { data.upToDateApplications }, Fill = Brushes.Green },
                new PieSeries { Title = "Out-of-date Applications", Values = new LiveCharts.ChartValues<int> { data.outOfDateApplications }, Fill = Brushes.Red }
            };

            // Update PieChart for Drivers
            this.pieChartDrivers.Series = new LiveCharts.SeriesCollection
            {
                new PieSeries { Title = "Up-to-date Drivers", Values = new LiveCharts.ChartValues<int> { data.upToDateDrivers }, Fill = Brushes.Blue },
                new PieSeries { Title = "Out-of-date Drivers", Values = new LiveCharts.ChartValues<int> { data.outOfDateDrivers }, Fill = Brushes.Orange }
            };
        }
    }
}
