using BossrScraper.Services.Scraper;
using System;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scheduler
{
    public class Scheduler : IScheduler
    {
        private readonly IWorldParser worldScraper;
        public Scheduler(IWorldParser worldScraper)
        {
            this.worldScraper = worldScraper;
        }
        public async Task Run()
        {
            while (true)
            {
                await worldScraper.Parse();
                await Task.Delay(TimeSpan.FromMinutes(30));
            }
        }
    }
}
