using System;
using System.IO;
using System.Windows;
using wpf_antivirus.Data;
using wpf_antivirus.pages;

namespace wpf_antivirus
{
    public partial class App : System.Windows.Application
    {
        private static database db;
        public static database Db
        {
            get
            {
                if (db == null)
                {
                    db = new database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "scanhistory.db"));
                }
                return db;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            APIKey window = new APIKey();
            window.Show();
        }
    }
}
