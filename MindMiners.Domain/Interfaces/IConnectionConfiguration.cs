using System.Data;

namespace MindMiners.Domain.Interfaces
{
    public interface IConnectionConfiguration
    {
        IDbConnection GetConnection();
    }
}
