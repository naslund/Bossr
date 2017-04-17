using BossrScraper.Factories;
using BossrScraper.Services.Comparers;
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
        private readonly IRestClient restClient;
        private readonly IWorldComparer worldComparer;

        public WorldScraper(
            IConfigurationFactory configurationFactory,
            IWorldParser worldParser,
            IDataFetcher dataFetcher,
            IRestClient restClient,
            IWorldComparer worldComparer)
        {
            configuration = configurationFactory.CreateConfiguration();
            this.worldParser = worldParser;
            this.dataFetcher = dataFetcher;
            this.restClient = restClient;
            this.worldComparer = worldComparer;
        }

        public async Task Scrape()
        {
            var url = configuration["TibiaWorldsUrl"];
            var response = await dataFetcher.FetchHttpResponse(url);
            var scrapeWorlds = await worldParser.Parse(response);
            var existingWorlds = await restClient.GetWorldsAsync();
            var missingWorlds = worldComparer.FindMissingWorlds(scrapeWorlds, existingWorlds);
            foreach (var world in missingWorlds)
                await restClient.PostWorldAsync(world);
        }
    }
}