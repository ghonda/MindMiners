using MindMiners.Domain.Entities;
using MindMiners.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MindMiners.Application
{
    public class HistoryApplication : IHistoryApplication
    {
        private readonly IFileHistoryRepository _fileHistoryRepository;

        public HistoryApplication(IFileHistoryRepository fileHistoryRepository)
        {
            _fileHistoryRepository = fileHistoryRepository;
        }
        public (byte[] fileBytes, string name) GetFile(int id)
        {
            return _fileHistoryRepository.GetFile(id);
        }

        public IEnumerable<FileHistory> GetFileHistory()
        {
            return _fileHistoryRepository.GetFileHistories();
        }

        public void RemoveFile(int id)
        {
            _fileHistoryRepository.RemoveFile(id);
        }
    }
}
