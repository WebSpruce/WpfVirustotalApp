using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using System.Security.Cryptography;
using VirusTotalNet.Results;
using VirusTotalNet;
using Microsoft.Win32;
using WPFCustomMessageBox;
using wpf_antivirus.pages;
using System.Windows.Navigation;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace wpf_antivirus
{
    public partial class MainWindow : Window
    {
        public VirusTotal virusTotal;
        public static MainWindow everything;
        public string key;
        public MainWindow()
        {
            InitializeComponent();
            everything = this;
            DataContext = this;

            listView.UnselectAll();

            key = APIKey.everything.keyFromTxtFile;
            virusTotal = new VirusTotal(key);
        }
        public string fileDropped { get; set; }
        public FileReport fileReport;
        private async void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && listView.Items.Count == 0)
            {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    Trace.WriteLine("file: " + files[0]);
                    fileDropped = files[0];
                    fileReport = await virusTotal.GetFileReportAsync(ToSHA256(fileDropped));
                    listView.Items.Add(new FileList() { id = (listView.Items.Count + 1), fileDropped = fileDropped, fileHash = fileReport.Resource }); 
            }
            else
            {
                MessageBox.Show("You can scan only one file.", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void Button_ScanFile(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listView.Items.Count != 0)
                {
                    if (fileReport.Positives > 0)   //amount of threat
                    {
                        MessageBox.Show($"I found something! Be careful.\nFilePath: {fileDropped}\n Found {fileReport.Positives} threat!", "Result of scanning this file", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Your file is save.\nFilePath: {fileDropped}\nFound {fileReport.Positives} threat.", "Result of scanning this file", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    Trace.WriteLine("File: " + fileReport.Positives + ", " + fileReport);
                    await App.Db.SaveScanResult(new scanhistory
                    {
                        path = fileDropped,
                        amount = fileReport.Positives,
                        time = DateTime.Now.TimeOfDay
                    });
                }
                else
                {
                    MessageBox.Show("There's no item in list.", "Something's wrong.", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"ScanFile error: {ex}");
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
        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedItems.Count!=0)
            {
                FileList selectedFile = (FileList)listView.SelectedItems[0];
                //MessageBox.Show($"ID:{selectedFile.id} \nPath:{selectedFile.fileDropped}","Selected Item",MessageBoxButton.OK,MessageBoxImage.Information);
                MessageBoxResult MBResult = CustomMessageBox.ShowYesNo($"ID:{selectedFile.id} \nPath:{selectedFile.fileDropped}", "Selected Item", "Okay", "Remove");
                if (MBResult == MessageBoxResult.Yes)
                {
                    listView.UnselectAll();
                }
                else if (MBResult == MessageBoxResult.No)
                {
                    listView.Items.RemoveAt(selectedFile.id - 1);
                    fileDropped = string.Empty;
                    fileReport = null;
                }
            }
            
        }

        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    fileDropped = openFileDialog.FileName;
                    fileReport = await virusTotal.GetFileReportAsync(ToSHA256(fileDropped));
                    listView.Items.Add(new FileList() { id = (listView.Items.Count + 1), fileDropped = fileDropped, fileHash = fileReport.Resource });
                }
                
            }
        }

        private void Button_ScanUrl(object sender, RoutedEventArgs e)
        {
            ScanUrl scanurl = new ScanUrl();
            scanurl.Show();
        }

        private void Button_History(object sender, RoutedEventArgs e)
        {
            HistoryPage hpage = new HistoryPage();
            hpage.Show();
        }
        private void Button_Key(object sender, RoutedEventArgs e)
        {
            APIKey gkey = new APIKey();
            gkey.Show();
        }
    }
    public class FileList
    {
        public int id { get; set; }
        public string fileDropped { get; set; }
        public string fileHash { get; set; }
    }
}
