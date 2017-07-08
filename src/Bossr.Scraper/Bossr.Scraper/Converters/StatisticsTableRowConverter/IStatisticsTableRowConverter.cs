using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;

namespace Bossr.Scraper.Converters
{
    public interface IStatisticsTableRowConverter
    {
        IEnumerable<IStatistic> ConvertToStatistics(IEnumerable<IStatisticsTableRow> killStatsTableRows, IEnumerable<IWorld> worlds, IEnumerable<ICreature> creatures, Scrape scrape);
    }
}
