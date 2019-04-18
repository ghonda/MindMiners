using System.IO;

namespace MindMiners.Domain.Interfaces
{
    public interface ISynchronizationApplication
    {
        string SubtitleSync(Stream strStream, double offSetSeconds = 0);
    }
}
