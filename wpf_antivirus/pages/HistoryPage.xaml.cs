using SQLite;
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
            string table = await App.Db.CheckIfTableIsCreated();
            if (!string.IsNullOrEmpty(table))
            {
                if (await App.Db.GetScanHistory() != null || App.Db.GetScanHistory().Result.Count > 0)
                {
                    listview.ItemsSource = null;
                    listview.ItemsSource = await App.Db.GetScanHistory();
                }
            }
            else
            {
                CreateTableResult createTableResult = await App.Db.CreateTable();
                if (createTableResult==CreateTableResult.Created || createTableResult == CreateTableResult.Migrated)
                {
                    MessageBox.Show("The table didn't find. The table has been created. Please try again.", "Error");
                }
                else
                {
                    MessageBox.Show("The table didn't find. There is a problem for create table.", "Error");
                }
            }
        }
    }
}
