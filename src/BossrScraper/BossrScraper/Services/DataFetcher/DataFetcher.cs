using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.DataFetcher
{
    public class DataFetcher : IDataFetcher
    {
        public async Task<HttpResponseMessage> FetchHttpResponse(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                return response;
            }
        }
    }
}
