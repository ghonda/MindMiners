using MindMiners.Domain.Entities;
using System.Collections.Generic;
using System.IO;

namespace MindMiners.Domain.Interfaces
{
    public interface ISrtParser
    {
        IList<SubtitleItem> ParseToSubtitleItemList(Stream srtStream, int offsetMilliSeconds = 0);
    }
}
