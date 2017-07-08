using Bossr.Scraper.Converters;
using Bossr.Scraper.Factories;
using Bossr.Scraper.Services.Scrapers;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public interface IScheduler
    {
        Task Run();
    }

    public class Scheduler : IScheduler
    {
        private readonly IWorldScraper worldScraper;
        private readonly IStatisticsTableRowScraper statsScraper;
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly IDateTimeFactory dateTimeFactory;

        public Scheduler(
            IWorldScraper worldScraper,
            IStatisticsTableRowScraper statsScraper,
            IConfigurationFactory configurationFactory,
            IRestClient restClient,
            IDateTimeFactory dateTimeFactory)
        {
            configuration = configurationFactory.CreateConfiguration();
            this.worldScraper = worldScraper;
            this.statsScraper = statsScraper;
            this.restClient = restClient;
            this.dateTimeFactory = dateTimeFactory;
        }

        public async Task Run()
        {
            while (true)
            {
                if (DateTime.UtcNow.Hour > 2 && await AreThereNewStats())
                {
                    await worldScraper.Scrape();
                    await statsScraper.Scrape();
                }
                var delay = int.Parse(configuration["ScrapeDelayMs"]);
                await Task.Delay(delay);
            }
        }

        private async Task<bool> AreThereNewStats()
        {
            var latestScrape = await restClient.GetLatestScrapeAsync();
            if (latestScrape == null)
                return true;
            
            var yesterday = dateTimeFactory.GetYesterdaysDate();
            return latestScrape.Date != yesterday;
        }
    }
}