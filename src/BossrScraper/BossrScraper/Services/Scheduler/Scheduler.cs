using BossrScraper.Services.Scrapers;
using System;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public class Scheduler : IScheduler
    {
        private readonly IWorldScraper worldScraper;
        private readonly IStatsScraper statsScraper;

        public Scheduler(
            IWorldScraper worldScraper,
            IStatsScraper statsScraper)
        {
            this.worldScraper = worldScraper;
            this.statsScraper = statsScraper;
        }

        public async Task Run()
        {
            while (true)
            {
                await worldScraper.Scrape();
                await statsScraper.Scrape();
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
        }
    }
}