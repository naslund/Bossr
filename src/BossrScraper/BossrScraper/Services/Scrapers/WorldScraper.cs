using BossrScraper.Factories;
using BossrScraper.Services.DataFetcher;
using BossrScraper.Services.Parsers;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scrapers
{
    public class WorldScraper : IWorldScraper
    {
        private readonly IConfiguration configuration;
        private readonly IWorldParser worldParser;
        private readonly IDataFetcher dataFetcher;
        public WorldScraper(
            IConfigurationFactory configurationFactory,
            IWorldParser worldParser, 
            IDataFetcher dataFetcher)
        {
            configuration = configurationFactory.CreateConfiguration();
            this.worldParser = worldParser;
            this.dataFetcher = dataFetcher;
        }

        public async Task Scrape()
        {
            var url = configuration["TibiaWorldsUrl"];
            var response = await dataFetcher.FetchHttpResponse(url);
            var worlds = await worldParser.Parse(response);
        }
    }
}
