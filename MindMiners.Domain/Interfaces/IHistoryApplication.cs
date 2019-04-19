using System.Collections.Generic;
using MindMiners.Domain.Entities;

namespace MindMiners.Domain.Interfaces
{
    public interface IHistoryApplication
    {
        IEnumerable<FileHistory> GetFileHistory();
        (byte[] fileBytes, string name) GetFile(int id);
        void RemoveFile(int id);
    }
}
