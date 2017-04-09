using System.Threading.Tasks;

namespace BossrScraper.Services.Scrapers.WorldScraper
{
    public interface IWorldScraper
    {
        Task Scrape();
    }
}