using MindMiners.Domain.Interfaces;
using System;
using System.IO;

namespace MindMiners.Application
{
    public class SynchronizationApplication : ISynchronizationApplication
    {
        private readonly ISrtParser _srtParser;

        public SynchronizationApplication(ISrtParser srtParser)
        {
            _srtParser = srtParser;
        }
        public void SubtitleSync(Stream strStream, double offSetSeconds = 0)
        {

            var offSetMilliSeconds = ConvertSecondsToMilliseconds(offSetSeconds);
            //converter em entity
            var subtitleList = _srtParser.ParseToSubtitleItemList(strStream, offSetMilliSeconds);
            //salvar no banco
            //retornar dowload;
        }

        private static int ConvertSecondsToMilliseconds(double seconds)
        {
            return Convert.ToInt32(TimeSpan.FromSeconds(seconds).TotalMilliseconds);
        }
    }
}
