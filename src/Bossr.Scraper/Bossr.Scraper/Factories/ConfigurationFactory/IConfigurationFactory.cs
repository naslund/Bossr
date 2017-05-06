using Microsoft.Extensions.Configuration;

namespace Bossr.Scraper.Factories
{
    public interface IConfigurationFactory
    {
        IConfiguration CreateConfiguration();
    }
}