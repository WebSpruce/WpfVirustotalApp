using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using wpf_antivirus.Models;

namespace wpf_antivirus.Data
{
    public class database
    {
        private readonly SQLiteAsyncConnection _connection;
        public database(string db_path)
        {
            _connection = new SQLiteAsyncConnection(db_path);
            _connection.CreateTableAsync<scanhistory>();
        }
        public Task<string> CheckIfTableIsCreated()
        {
            return _connection.ExecuteScalarAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name='scanhistory';");
        }
        public Task<CreateTableResult> CreateTable()
        {
            return _connection.CreateTableAsync<scanhistory>();
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
