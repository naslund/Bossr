using Bossr.Scraper.Factories;
using Bossr.Scraper.Services;
using Bossr.Scraper.Services.Comparers;
using Bossr.Scraper.Services.Converters;
using Bossr.Scraper.Services.Parsers;
using Bossr.Scraper.Services.Scrapers;
using Microsoft.Extensions.DependencyInjection;

namespace Bossr.Scraper
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IConfigurationFactory, ConfigurationFactory>()
                .AddTransient<IYesterdayLocalDateFactory, YesterdayLocalDateFactory>()

                .AddTransient<IScheduler, Scheduler>()
                .AddTransient<IDataFetcher, DataFetcher>()
                .AddSingleton<IRestClient, RestClient>()

                .AddTransient<IWorldScraper, WorldScraper>()
                .AddTransient<IWorldParser, WorldParser>()
                .AddTransient<IWorldComparer, WorldComparer>()

                .AddTransient<IStatisticScraper, StatisticScraper>()
                .AddTransient<IStatisticParser, StatisticParser>()
                .AddTransient<IStatisticConverter, StatisticConverter>()

                .AddTransient<ICreatureComparer, CreatureComparer>()

                .BuildServiceProvider();

            var scraper = serviceProvider.GetService<IScheduler>();
            scraper.Run().GetAwaiter().GetResult();
        }
    }
}