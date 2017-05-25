using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;

namespace Bossr.Scraper.Comparers
{
    public interface ICreatureComparer
    {
        IEnumerable<ICreature> FindMissingCreatures(IEnumerable<IStatisticsTableRow> killStatsTableRows, IEnumerable<ICreature> existingCreatures);
    }
}
