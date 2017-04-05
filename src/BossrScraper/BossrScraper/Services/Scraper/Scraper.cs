using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scraper
{
    public class Scraper : IScraper
    {
        public async Task<HttpResponseMessage> Scrape(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return response;
            }
        }
    }
}
