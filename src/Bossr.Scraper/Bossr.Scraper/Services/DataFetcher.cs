using Bossr.Scraper.Factories;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public interface IDataFetcher
    {
        Task<HttpResponseMessage> FetchHttpResponse(string url);
    }

    public class DataFetcher : IDataFetcher
    {
        private readonly IConfiguration configuration;

        public DataFetcher(IConfigurationFactory configurationFactory)
        {
            configuration = configurationFactory.CreateConfiguration();
        }

        public async Task<HttpResponseMessage> FetchHttpResponse(string url)
        {
            var delay = int.Parse(configuration["DataFetcherDelayMs"]);
            await Task.Delay(delay);

            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }
    }
}