using BossrScraper.Factories;
using BossrScraper.Services;
using BossrScraper.Services.Scheduler;
using BossrScraper.Services.Scraper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BossrScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IConfigurationFactory, ConfigurationFactory>()
                .AddTransient<IScheduler, Scheduler>()
                .AddTransient<IWorldParser, WorldParser>()
                .AddTransient<IScraper, Scraper>()
                .BuildServiceProvider();

            var scraper = serviceProvider.GetService<IScheduler>();
            scraper.Run().GetAwaiter().GetResult();
        }
    }
}