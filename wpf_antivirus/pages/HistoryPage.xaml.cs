using System.Windows;

namespace wpf_antivirus.pages
{
    public partial class HistoryPage : Window
    {
        public HistoryPage()
        {
            InitializeComponent();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listview.ItemsSource = null;
            listview.ItemsSource = await App.Db.GetScanHistory();
        }
    }
}
