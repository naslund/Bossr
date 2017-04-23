using BossrScraper.Converters;
using BossrScraper.Factories;
using BossrScraper.Models.Entities;
using BossrScraper.Services.Comparers;
using BossrScraper.Services.Converters;
using BossrScraper.Services.Parsers;
using Microsoft.Extensions.Configuration;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossrScraper.Services.Scrapers
{
    public class StatisticScraper : IStatisticScraper
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly IDataFetcher dataFetcher;
        private readonly IStatisticParser statsParser;
        private readonly ICreatureComparer creatureComparer;
        private readonly IStatisticConverter statisticConverter;
        private readonly IYesterdayLocalDateFactory yesterdayLocalDateFactory;

        public StatisticScraper(
            IConfigurationFactory configurationFactory,
            IRestClient restClient,
            IDataFetcher dataFetcher,
            IStatisticParser statsParser,
            ICreatureComparer creatureComparer,
            IStatisticConverter statisticConverter,
            IYesterdayLocalDateFactory yesterdayLocalDateFactory)
        {
            configuration = configurationFactory.CreateConfiguration();
            this.restClient = restClient;
            this.dataFetcher = dataFetcher;
            this.statsParser = statsParser;
            this.creatureComparer = creatureComparer;
            this.statisticConverter = statisticConverter;
            this.yesterdayLocalDateFactory = yesterdayLocalDateFactory;
        }

        public async Task Scrape()
        {
            var worlds = await restClient.GetWorldsAsync();
            var statistics = await ScrapeStats(worlds);

            var creatures = await restClient.GetCreaturesAsync();
            creatures = await PersistCreatures(creatures, statistics);
            
            var scrapeDto = new ScrapeDto { Date = yesterdayLocalDateFactory.GetYesterdaysDate().ToIsoString() };
            await restClient.PostScrapeAsync(scrapeDto);

            var spawns = statisticConverter.ConvertToSpawns(statistics, worlds, creatures, scrapeDto);
            foreach (var spawn in spawns)
                await restClient.PostSpawnAsync(spawn);
        }

        private async Task<IEnumerable<ICreature>> PersistCreatures(IEnumerable<ICreature> creatures, IEnumerable<IStatistic> statistics)
        {
            var missingCreatures = creatureComparer.FindMissingCreatures(statistics, creatures).ToList();
            foreach (var creature in missingCreatures)
                await restClient.PostCreatureAsync(creature);
            return creatures.Concat(missingCreatures);
        }

        private async Task<IEnumerable<IStatistic>> ScrapeStats(IEnumerable<IWorld> worlds)
        {
            var stats = new List<IStatistic>();
            foreach (var world in worlds)
            {
                var baseUrl = configuration["TibiaStatsUrl"];
                var response = await dataFetcher.FetchHttpResponse($"{baseUrl}{world.Name}");
                stats.AddRange(await statsParser.Parse(response, world.Id));
            }
            return stats;
        }
    }
}