using BossrScraper.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BossrScraper.Services.Comparers
{
    public class CreatureComparer : ICreatureComparer
    {
        public IEnumerable<ICreature> FindMissingCreatures(IEnumerable<IStatistic> statistics, IEnumerable<ICreature> existingCreatures)
        {
            return statistics
                .Where(x => !existingCreatures.Any(y => y.Name == x.CreatureName))
                .GroupBy(x => x.CreatureName)
                .Select(x => new Creature { Name = x.First().CreatureName });
        }
    }
}
