using Microsoft.VisualBasic;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_antivirus
{
    public class scanhistory
    {
        [AutoIncrement,PrimaryKey]
        public int id { get; set; }
        public string path { get; set; }
        public int amount { get; set; }
        public TimeSpan time { get; set; }
    }
}
