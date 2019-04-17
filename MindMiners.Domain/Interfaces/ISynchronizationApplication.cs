using System.IO;

namespace MindMiners.Domain.Interfaces
{
    public interface ISynchronizationApplication
    {
        void SubtitleSync(Stream strStream, double offSetSeconds = 0);
    }
}
