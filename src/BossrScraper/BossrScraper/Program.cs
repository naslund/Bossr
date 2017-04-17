﻿using BossrScraper.Factories;
using BossrScraper.Services;
using BossrScraper.Services.Comparers;
using BossrScraper.Services.Parsers;
using BossrScraper.Services.Scrapers;
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

                .AddTransient<IStatsScraper, StatsScraper>()
                .AddTransient<IStatsParser, StatsParser>()

                .AddTransient<ICreatureComparer, CreatureComparer>()

                .BuildServiceProvider();

            var scraper = serviceProvider.GetService<IScheduler>();
            scraper.Run().GetAwaiter().GetResult();
        }
    }
}