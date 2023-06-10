using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpf_antivirus.pages;
using static System.Net.Mime.MediaTypeNames;

namespace wpf_antivirus
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
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
    }
}
