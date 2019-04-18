using Dapper;
using MindMiners.Domain.Entities;
using MindMiners.Domain.Interfaces;
using System.Collections.Generic;

namespace MindMiners.Data.Repositories
{
    public class FileHistoryRepository : IFileHistoryRepository
    {
        private readonly IConnectionConfiguration _connectionConfiguration;

        public FileHistoryRepository(IConnectionConfiguration connectionConfiguration)
        {
            _connectionConfiguration = connectionConfiguration;
        }

        public void InsertHistory(FileHistory fileHistory)
        {
            using (var dbConnection = _connectionConfiguration.GetConnection())
            {
                dbConnection.Execute(@"INSERT INTO FileHistory(name, newname, offset)
                    VALUES (@name, @newname, @offset)",
                    new
                    {
                        @name = fileHistory.Name,
                        @newname = fileHistory.NewName,
                        @offset = fileHistory.Offset
                    });
            }
        }

        public IEnumerable<FileHistory> GetFileHistories()
        {
            using (var dbConnection = _connectionConfiguration.GetConnection())
                return dbConnection.Query<FileHistory>("SELECT * FROM FileHistory");
        }
    }
}
