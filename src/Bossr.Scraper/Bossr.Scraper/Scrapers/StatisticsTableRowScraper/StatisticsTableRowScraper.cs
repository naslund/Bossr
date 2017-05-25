using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Comparers;
using Bossr.Scraper.Converters;
using Bossr.Scraper.Factories;
using Bossr.Scraper.Models.Entities;
using Bossr.Scraper.Parsers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services.Scrapers
{
    public class StatisticsTableRowScraper : IStatisticsTableRowScraper
    {
        private readonly IConfiguration configuration;
        private readonly IRestClient restClient;
        private readonly IDataFetcher dataFetcher;
        private readonly IStatisticsTableRowParser statsParser;
        private readonly ICreatureComparer creatureComparer;
        private readonly IStatisticsTableRowConverter statisticConverter;
        private readonly IYesterdayLocalDateFactory yesterdayLocalDateFactory;

        public StatisticsTableRowScraper(
            IConfigurationFactory configurationFactory,
            IRestClient restClient,
            IDataFetcher dataFetcher,
            IStatisticsTableRowParser statsParser,
            ICreatureComparer creatureComparer,
            IStatisticsTableRowConverter statisticConverter,
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
            var killStatsTableRows = await ScrapeStats(worlds);

            var creatures = await restClient.GetCreaturesAsync();
            creatures = await PersistCreatures(creatures, killStatsTableRows);

            var scrapeDto = new ScrapeDto { Date = yesterdayLocalDateFactory.GetYesterdaysDate().ToIsoString() };
            await restClient.PostScrapeAsync(scrapeDto);
            Console.WriteLine($"{scrapeDto.Id} {scrapeDto.Date}");

            var statistics = statisticConverter.ConvertToStatistics(killStatsTableRows, worlds, creatures, scrapeDto);
            foreach (var statistic in statistics)
                await restClient.PostStatisticAsync(statistic);
        }

        private async Task<IEnumerable<ICreature>> PersistCreatures(IEnumerable<ICreature> creatures, IEnumerable<IStatisticsTableRow> statistics)
        {
            var missingCreatures = creatureComparer.FindMissingCreatures(statistics, creatures).ToList();
            foreach (var creature in missingCreatures)
                await restClient.PostCreatureAsync(creature);
            return creatures.Concat(missingCreatures);
        }

        private async Task<IEnumerable<IStatisticsTableRow>> ScrapeStats(IEnumerable<IWorld> worlds)
        {
            var stats = new List<IStatisticsTableRow>();
            foreach (var world in worlds)
            {
                stats.AddRange(await ScrapeStatsForWorld(world));
            }
            return stats;
        }

        private async Task<IEnumerable<IStatisticsTableRow>> ScrapeStatsForWorld(IWorld world)
        {
            var baseUrl = configuration["TibiaStatsUrl"];
            int maximumAttempts = int.Parse(configuration["ScrapeAttemptsMax"]);
            IEnumerable<IStatisticsTableRow> result = null;
            int attempt = 0;
            while (result == null)
            {
                attempt++;

                try
                {
                    var response = await dataFetcher.FetchHttpResponse($"{baseUrl}{world.Name}");
                    result = await statsParser.Parse(response, world.Id);
                }
                catch (Exception)
                {
                    if (maximumAttempts <= attempt)
                        throw;
                }
            }

            return result;
        }
    }
}