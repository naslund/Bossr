using BossrScraper.Factories;
using BossrScraper.Models.Entities;
using BossrScraper.Services.Comparers;
using BossrScraper.Services.Parsers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scrapers
{
    public class StatsScraper : IStatsScraper
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly IDataFetcher dataFetcher;
        private readonly IStatsParser statsParser;
        private readonly ICreatureComparer creatureComparer;

        public StatsScraper(
            IConfigurationFactory configurationFactory,
            IRestClient restClient,
            IDataFetcher dataFetcher,
            IStatsParser statsParser,
            ICreatureComparer creatureComparer)
        {
            configuration = configurationFactory.CreateConfiguration();
            this.restClient = restClient;
            this.dataFetcher = dataFetcher;
            this.statsParser = statsParser;
            this.creatureComparer = creatureComparer;
        }

        public async Task Scrape()
        {
            var stats = new List<IStatistic>();

            var worlds = await restClient.GetWorldsAsync();
            foreach (var world in worlds)
                stats.AddRange(await ScrapeStats(world));

            var creatures = await restClient.GetCreaturesAsync();
            var missingCreatures = creatureComparer.FindMissingCreatures(stats, creatures).ToList();
            foreach (var creature in missingCreatures)
                await restClient.PostCreatureAsync(creature);
            creatures = creatures.Concat(missingCreatures);

            var spawns = new List<ISpawn>();
            var currentStats = stats
                .Where(x => x.CreaturesKilled > 0 || x.PlayersKilled > 0)
                .Where(x => creatures.Single(y => y.Name == x.CreatureName).IsMonitored)
                .Where(x => worlds.Single(y => y.Id == x.WorldId).IsMonitored)
                .ToList();

            foreach (var stat in currentStats)
            {
                if (stat.CreaturesKilled == 0) stat.CreaturesKilled = 1;
                while (stat.CreaturesKilled > 0)
                {
                    spawns.Add(new Spawn
                    {
                        WorldId = stat.WorldId,
                        CreatureId = creatures.Single(x => x.Name == stat.CreatureName).Id
                    });
                    stat.CreaturesKilled--;
                }
            }


        }

        private async Task<IEnumerable<IStatistic>> ScrapeStats(IWorld world)
        {
            var baseUrl = configuration["TibiaStatsUrl"];
            var response = await dataFetcher.FetchHttpResponse($"{baseUrl}{world.Name}");
            return await statsParser.Parse(response, world.Id);
        }
    }
}