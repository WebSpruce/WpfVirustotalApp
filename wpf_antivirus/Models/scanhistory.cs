using SQLite;
using System;

namespace wpf_antivirus.Models
{
    public class scanhistory
    {
        [AutoIncrement, PrimaryKey]
        public int id { get; set; }
        public string path { get; set; }
        public int amount { get; set; }
        public TimeSpan time { get; set; }
    }
}
