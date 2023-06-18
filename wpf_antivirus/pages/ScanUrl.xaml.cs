using System;
using System.Windows;
using VirusTotalNet.Results;
using VirusTotalNet;
using System.Diagnostics;

namespace wpf_antivirus.pages
{
    public partial class ScanUrl : Window
    {
        public static ScanUrl everything;
        public VirusTotal virusTotal2;
        public UrlReport urlReport;
        public ScanUrl()
        {
            InitializeComponent();
            everything = this;
            Trace.WriteLine($"Key: {MainWindow.everything.key}");
            virusTotal2 = new VirusTotal(MainWindow.everything.key);
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(url.Text))
                {
                    urlReport = await virusTotal2.GetUrlReportAsync(url.Text);
                    if (urlReport.Positives > 0)   //amount of threat
                    {
                        MessageBox.Show($"I found something! Be careful.\nUrl link: {url.Text}\n Found {urlReport.Positives} threat!", "Result of scanning this link", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"The link is save.\nUrl link: {url.Text}\nFound {urlReport.Positives} threat.", "Result of scanning this link", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    await App.Db.SaveScanResult(new scanhistory
                    {
                        path = url.Text,
                        amount = urlReport.Positives,
                        time = DateTime.Now.TimeOfDay
                    });
                }
                else
                {
                    MessageBox.Show("Something is wrong with input field.","Information",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Something is wrong here: {ex}");
            }
            
            
        }
    }
}
