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

        public (byte[] fileBytes, string name) GetFile(int id)
        {
            using (var dbConnection = _connectionConfiguration.GetConnection())
                return dbConnection.QueryFirstOrDefault<(byte[] fileBytes, string name)>("SELECT file, name FROM FileHistory where id = @id", new {id});
        }

        public void RemoveFile(int id)
        {
            using (var dbConnection = _connectionConfiguration.GetConnection())
                dbConnection.Execute("Delete FROM FileHistory where id = @id", new {id});
        }
    }
}
