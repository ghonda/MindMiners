using MindMiners.CrossCutting.Infrastructure.Utils;
using MindMiners.Domain.Interfaces;
using System;
using System.IO;
using System.Text;

namespace MindMiners.Application
{
    public class SynchronizationApplication : ISynchronizationApplication
    {
        private readonly ISrtParser _srtParser;
        private readonly IFileHistoryRepository _fileHistoryRepository;

        public SynchronizationApplication(ISrtParser srtParser, IFileHistoryRepository fileHistoryRepository)
        {
            _srtParser = srtParser;
            _fileHistoryRepository = fileHistoryRepository;
        }
        public byte[] SubtitleSync(Stream strStream,string fileName, double offSetSeconds = 0)
        {
            var subtitleItemList = new StringBuilder();
            var offSetMilliSeconds = Helper.ConvertSecondsToMilliseconds(offSetSeconds);
            var subtitleList = _srtParser.ParseToSubtitleItemList(strStream, offSetMilliSeconds);
            subtitleList.ForEach(c => subtitleItemList.AppendLine(c.ToString()));
            var newFileBytes = Encoding.ASCII.GetBytes(subtitleItemList.ToString());
            _fileHistoryRepository.InsertHistory(fileName,newFileBytes, offSetSeconds);

            return newFileBytes;
        }
    }
}
