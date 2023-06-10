using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VirusTotalNet.Results;
using VirusTotalNet;
using System.Diagnostics;

namespace wpf_antivirus.pages
{
    /// <summary>
    /// Interaction logic for ScanUrl.xaml
    /// </summary>
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
