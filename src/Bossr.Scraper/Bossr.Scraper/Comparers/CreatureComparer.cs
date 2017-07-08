using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Scraper.Comparers
{
    public interface ICreatureComparer
    {
        IEnumerable<ICreature> FindMissingCreatures(IEnumerable<IStatisticsTableRow> killStatsTableRows, IEnumerable<ICreature> existingCreatures);
    }

    public class CreatureComparer : ICreatureComparer
    {
        public IEnumerable<ICreature> FindMissingCreatures(IEnumerable<IStatisticsTableRow> killStatsTableRows, IEnumerable<ICreature> existingCreatures)
        {
            return killStatsTableRows
                .Where(x => !existingCreatures.Any(y => y.Name == x.CreatureName))
                .GroupBy(x => x.CreatureName)
                .Select(x => new Creature { Name = x.First().CreatureName });
        }
    }
}
