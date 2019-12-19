using Bossr.Scraper.Comparers;
using Bossr.Scraper.Converters;
using Bossr.Scraper.Factories;
using Bossr.Scraper.Parsers;
using Bossr.Scraper.Services;
using Bossr.Scraper.Services.Scrapers;
using Microsoft.Extensions.DependencyInjection;

namespace Bossr.Scraper
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IConfigurationFactory, ConfigurationFactory>()
                .AddTransient<IDateTimeFactory, DateTimeFactory>()

                .AddTransient<IScheduler, Scheduler>()
                .AddTransient<IDataFetcher, DriverDataFetcher>()
                .AddSingleton<IRestClient, RestClient>()

                .AddTransient<IWorldScraper, WorldScraper>()
                .AddTransient<IWorldParser, WorldParser>()
                .AddTransient<IWorldComparer, WorldComparer>()

                .AddTransient<IStatisticsTableRowScraper, StatisticsTableRowScraper>()
                .AddTransient<IStatisticsTableRowParser, StatisticsTableRowParser>()
                .AddTransient<IStatisticsTableRowConverter, StatisticsTableRowConverter>()

                .AddTransient<ICreatureComparer, CreatureComparer>()

                .BuildServiceProvider();

            var scraper = serviceProvider.GetService<IScheduler>();
            scraper.Run().GetAwaiter().GetResult();
        }
    }
}