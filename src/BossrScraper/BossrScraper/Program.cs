using BossrScraper.Factories.ConfigurationFactory;
using BossrScraper.Services.Comparers.WorldComparer;
using BossrScraper.Services.DataFetcher;
using BossrScraper.Services.Parsers.WorldParser;
using BossrScraper.Services.RestClient;
using BossrScraper.Services.Scheduler;
using BossrScraper.Services.Scrapers.WorldScraper;
using Microsoft.Extensions.DependencyInjection;

namespace BossrScraper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IConfigurationFactory, ConfigurationFactory>()
                .AddTransient<IScheduler, Scheduler>()
                .AddTransient<IDataFetcher, DataFetcher>()
                .AddTransient<IRestClient, RestClient>()

                .AddTransient<IWorldScraper, WorldScraper>()
                .AddTransient<IWorldParser, WorldParser>()
                .AddTransient<IWorldComparer, WorldComparer>()

                .BuildServiceProvider();

            var scraper = serviceProvider.GetService<IScheduler>();
            scraper.Run().GetAwaiter().GetResult();
        }
    }
}