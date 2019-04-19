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

        public void InsertHistory(string name, byte[] file, double offset)
        {
            using (var dbConnection = _connectionConfiguration.GetConnection())
            {
                dbConnection.Execute(@"INSERT INTO FileHistory(name, file, offset)
                    VALUES (@name, @file, @offset)",
                    new
                    {
                        name,
                        file,
                        offset
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
