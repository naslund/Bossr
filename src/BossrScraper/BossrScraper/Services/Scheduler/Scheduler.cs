using BossrScraper.Services.Scrapers;
using System;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scheduler
{
    public class Scheduler : IScheduler
    {
        private readonly IWorldScraper worldScraper;
        public Scheduler(IWorldScraper worldScraper)
        {
            this.worldScraper = worldScraper;
        }
        public async Task Run()
        {
            while (true)
            {
                await worldScraper.Scrape();
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
        }
    }
}
