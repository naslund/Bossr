using Microsoft.Extensions.Configuration;

namespace BossrScraper.Factories
{
    public interface IConfigurationFactory
    {
        IConfiguration CreateConfiguration();
    }
}
