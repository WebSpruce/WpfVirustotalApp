using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace wpf_antivirus.pages
{
    public partial class APIKey : Window
    {
        public static APIKey everything;
        public string json;
        public string path;
        public string filepath;
        public string keyFromTxtFile;
        public APIKey()
        {
            InitializeComponent();
            everything = this;

            LoadApiKey();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gkey.Text.Length == 64)
                {
                    File.WriteAllText(filepath, gkey.Text);
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Api key is too short.", "Something is wrong.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {
                Trace.WriteLine($"new Key error: {ex}");
            }
        }
        private void LoadApiKey()
        {
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                filepath = $"{path}/apikey.txt";
                if (!File.Exists(filepath))
                {
                    File.Create(filepath);
                }
                else
                {
                    keyFromTxtFile = File.ReadAllText(filepath);
                    gkey.Text = keyFromTxtFile;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Load Key error: {ex}");
            }
        }
    }
}
