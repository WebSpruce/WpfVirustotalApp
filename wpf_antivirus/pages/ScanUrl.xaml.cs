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

namespace wpf_antivirus.pages
{
    /// <summary>
    /// Interaction logic for ScanUrl.xaml
    /// </summary>
    public partial class ScanUrl : Window
    {
        public ScanUrl()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(url.Text))
            {
                url.Text = "Your url";
                url.Foreground = Brushes.Gray;
            }
        }

        private void url_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!string.IsNullOrEmpty(url.Text))
            {
                url.Text = "";
                url.Foreground = Brushes.Black;
            }
        }

        private void url_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(url.Text))
            {
                url.Text = "Your url";
                url.Foreground = Brushes.Gray;
            }
        }
        public VirusTotal virusTotal;
        public UrlReport urlReport;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(url.Text))
            {
                urlReport = await virusTotal.GetUrlReportAsync(url.Text);
                if (urlReport.Positives > 0)   //amount of threat
                {
                    MessageBox.Show($"I found something! Be careful.\nUrl link: {url.Text}\n Found {urlReport.Positives} threat!", "Result of scanning this link", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"The link is save.\nUrl link: {url.Text}\nFound {urlReport.Positives} threat.", "Result of scanning this link", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            
        }
    }
}
