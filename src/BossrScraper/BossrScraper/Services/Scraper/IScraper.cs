using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scraper
{
    public interface IScraper
    {
        Task<HttpResponseMessage> Scrape(string url);
    }
}
