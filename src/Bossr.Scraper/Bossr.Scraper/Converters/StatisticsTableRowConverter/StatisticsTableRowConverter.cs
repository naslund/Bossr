using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Scraper.Converters
{
    public class StatisticsTableRowConverter : IStatisticsTableRowConverter
    {
        public IEnumerable<IStatistic> ConvertToStatistics(IEnumerable<IStatisticsTableRow> statisticTableRow, IEnumerable<IWorld> worlds, IEnumerable<ICreature> creatures, Scrape scrape)
        {
            return statisticTableRow
                .Where(x => x.CreaturesKilled > 0 || x.PlayersKilled > 0)
                .Where(x => creatures.Single(y => y.Name == x.CreatureName).IsMonitored)
                .Where(x => worlds.Single(y => y.Id == x.WorldId).IsMonitored)
                .Select(x => new Statistic
                {
                    WorldId = x.WorldId,
                    ScrapeId = scrape.Id,
                    CreatureId = creatures.Single(y => y.Name == x.CreatureName).Id,
                    Amount = x.CreaturesKilled == 0 ? 1 : x.CreaturesKilled
                });
        }
    }
}
