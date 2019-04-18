using Dapper;
using MindMiners.Domain.Interfaces;

namespace MindMiners.Data.Repositories
{
    public class DbInit
    {
        public static void CreateDb(IConnectionConfiguration connectionConfiguration)
        {
            using (var dbConnection = connectionConfiguration.GetConnection())
            {
                dbConnection.Execute(@"
                CREATE TABLE IF NOT EXISTS [FileHistory] (
                    [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [name] NVARCHAR(128) NOT NULL,
                    [newname] NVARCHAR(128) NOT NULL,
                    [offset] REAL NOT NULL,
                    [CreateDate] TIMESTAMP DEFAULT CURRENT_TIMESTAMP)");
            }
        }
    }
}
