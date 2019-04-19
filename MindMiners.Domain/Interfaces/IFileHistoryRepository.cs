using MindMiners.Domain.Entities;
using System.Collections.Generic;

namespace MindMiners.Domain.Interfaces
{
    public interface IFileHistoryRepository
    {
        void InsertHistory(string name, byte[] file, double offset);
        IEnumerable<FileHistory> GetFileHistories();
    }
}
