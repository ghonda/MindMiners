using MindMiners.Domain.Entities;
using System.Collections.Generic;

namespace MindMiners.Domain.Interfaces
{
    public interface IFileHistoryRepository
    {
        void InsertHistory(FileHistory fileHistory);
        IEnumerable<FileHistory> GetFileHistories();
    }
}
