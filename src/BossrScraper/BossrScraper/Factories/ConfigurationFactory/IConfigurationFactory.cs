using Microsoft.Extensions.Configuration;

namespace BossrScraper.Factories.ConfigurationFactory
{
    public interface IConfigurationFactory
    {
        IConfiguration CreateConfiguration();
    }
}
