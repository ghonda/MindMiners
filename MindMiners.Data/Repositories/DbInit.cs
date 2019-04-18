using Dapper;
using System.Data.SQLite;
using System.IO;

namespace MindMiners.Data.Repositories
{
    public class DbInit
    {
        public static void CreateDb()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","dbs", "MindMiners.sqlite");
            if (!File.Exists(path))
                SQLiteConnection.CreateFile(path);

            using (var dbConnection = new SQLiteConnection($"Data Source={path};Version=3;"))
            {
                dbConnection.Execute(@"
                CREATE TABLE IF NOT EXISTS [FileHistory] (
                    [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [name] NVARCHAR(128) NOT NULL,
                    [newname] NVARCHAR(128) NOT NULL,
                    [offset] REAL NOT NULL,
                    [CreateDate] TIMESTAMP DEFAULT CURRENT_TIMESTAMP)");

                //    var result = dbConnection.Query<FileHistory>(@"
                //SELECT * FROM FileHistory");

                //        dbConnection.Execute(@"
                //INSERT INTO FileHistory(name, newname, offset)
                //    VALUES ('teste', 'teste223', 5.6)");

            }
        }
    }
}
