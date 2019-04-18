using MindMiners.Domain.Interfaces;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace MindMiners.Domain.Config
{
    public class ConnectionConfiguration : IConnectionConfiguration
    {
        private readonly string _dbPath;

        public ConnectionConfiguration(string dbPath)
        {
            _dbPath = dbPath;
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);
        }

        public IDbConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={_dbPath};Version=3;");
        }
    }
}
