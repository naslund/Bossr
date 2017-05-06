using System.Data;

namespace Bossr.Api.Factories
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}