using BossrScraper.Factories.ConfigurationFactory;
using BossrScraper.Services.DataFetcher;
using BossrScraper.Services.Parsers.WorldParser;
using BossrScraper.Services.Scheduler;
using BossrScraper.Services.Scrapers.WorldScraper;
using Microsoft.Extensions.DependencyInjection;

namespace BossrScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IConfigurationFactory, ConfigurationFactory>()
                .AddTransient<IScheduler, Scheduler>()
                .AddTransient<IDataFetcher, DataFetcher>()

                .AddTransient<IWorldScraper, WorldScraper>()
                .AddTransient<IWorldParser, WorldParser>()

                .BuildServiceProvider();

            var scraper = serviceProvider.GetService<IScheduler>();
            scraper.Run().GetAwaiter().GetResult();
        }
    }
}