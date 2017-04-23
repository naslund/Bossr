using System.Data;

namespace BossrApi.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}