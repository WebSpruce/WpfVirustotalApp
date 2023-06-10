using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using VirusTotalNet;

namespace wpf_antivirus.pages
{
    /// <summary>
    /// Interaction logic for GoogleKey.xaml
    /// </summary>
    public partial class GoogleKey : Window
    {
        public static GoogleKey everything;
        public GoogleKey()
        {
            InitializeComponent();
            everything = this;
            gkey.Text = MainWindow.everything.key;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.everything.key = (gkey.Text);
            Trace.WriteLine($"Key: {MainWindow.everything.key}");
        }
    }
}
