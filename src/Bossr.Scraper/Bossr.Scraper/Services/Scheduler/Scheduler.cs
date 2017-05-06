using Bossr.Scraper.Converters;
using Bossr.Scraper.Factories;
using Bossr.Scraper.Services.Scrapers;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public class Scheduler : IScheduler
    {
        private readonly IWorldScraper worldScraper;
        private readonly IStatisticScraper statsScraper;
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly IYesterdayLocalDateFactory yesterdayLocalDateFactory;

        public Scheduler(
            IWorldScraper worldScraper,
            IStatisticScraper statsScraper,
            IConfigurationFactory configurationFactory,
            IRestClient restClient,
            IYesterdayLocalDateFactory yesterdayLocalDateFactory)
        {
            configuration = configurationFactory.CreateConfiguration();
            this.worldScraper = worldScraper;
            this.statsScraper = statsScraper;
            this.restClient = restClient;
            this.yesterdayLocalDateFactory = yesterdayLocalDateFactory;
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

            var latestScrapeDate = latestScrape.Date.ToLocalDate();
            var yesterday = yesterdayLocalDateFactory.GetYesterdaysDate();
            return latestScrapeDate != yesterday;
        }
    }
}