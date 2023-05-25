using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace wpf_antivirus
{
    public class database
    {
        private readonly SQLiteAsyncConnection _connection;
        public database(string db_path)
        {
            _connection = new SQLiteAsyncConnection(db_path);
            _connection.CreateTableAsync<scanhistory>();
        }
        public Task<List<scanhistory>> GetScanHistory()
        {
            return _connection.Table<scanhistory>().ToListAsync();
        }
        public Task<int> SaveScanResult(scanhistory Scan)
        {
                return _connection.InsertAsync(Scan);
        }
    }
}
