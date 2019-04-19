using MindMiners.CrossCutting.Infrastructure.Utils;
using MindMiners.Domain.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MindMiners.Application
{
    public class SynchronizationApplication : ISynchronizationApplication
    {
        private readonly ISrtParser _srtParser;

        public SynchronizationApplication(ISrtParser srtParser)
        {
            _srtParser = srtParser;
        }
        public byte[] SubtitleSync(Stream strStream,string fileName, double offSetSeconds = 0)
        {
            var result = new StringBuilder();
            var offSetMilliSeconds = Helper.ConvertSecondsToMilliseconds(offSetSeconds);
            var subtitleList = _srtParser.ParseToSubtitleItemList(strStream, offSetMilliSeconds);
            subtitleList.ForEach(c => result.AppendLine(c.ToString()));
            return Encoding.ASCII.GetBytes(result.ToString());

            //TODO
            //salvar no banco
            //retornar dowload;
        }
    }
}
