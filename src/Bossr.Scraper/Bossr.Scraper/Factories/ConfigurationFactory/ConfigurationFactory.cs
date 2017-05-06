using Microsoft.Extensions.Configuration;
using System.IO;

namespace Bossr.Scraper.Factories
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        public IConfiguration CreateConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration;
        }
    }
}