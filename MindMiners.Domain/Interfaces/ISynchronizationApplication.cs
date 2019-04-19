using System.IO;

namespace MindMiners.Domain.Interfaces
{
    public interface ISynchronizationApplication
    {
        byte[] SubtitleSync(Stream strStream, string fileName ,double offSetSeconds = 0);
    }
}
