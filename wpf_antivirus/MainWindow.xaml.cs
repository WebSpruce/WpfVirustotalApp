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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;
using VirusTotalNet;
using Microsoft.Win32;

namespace wpf_antivirus
{
    public partial class MainWindow : Window
    {
        public VirusTotal virusTotal;
        public MainWindow()
        {
            InitializeComponent();
            virusTotal = new VirusTotal("c7e3e0f8e2c5684677408d0e039d321139390c4bc2f01ca091814ea213d00ec7");
        }
        public string fileDropped = string.Empty;
        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                Trace.WriteLine("file: " + files[0]);
                fileDropped = files[0];
            }
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        { 
            var fileReport = await virusTotal.GetFileReportAsync(ToSHA256(fileDropped));
            if (fileReport.Positives > 0)   //amount of threat
            {
                MessageBox.Show("I found something! Be careful."+"\n"+
                    "\n"+"FilePath: "+ fileDropped+"\n"+"Found "+fileReport.Positives+" threat!", "Result of scanning this file");
            }
            else
            {
                MessageBox.Show("Your file is save." + "\n" + "hash: "+
                    "\n" + "FilePath: " + fileDropped + "\n" + "Found " + fileReport.Positives + " threat.", "Result of scanning this file");
            }
                
        }
        public static string ToSHA256(string s)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
