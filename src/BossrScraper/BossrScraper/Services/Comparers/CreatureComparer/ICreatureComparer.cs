using BossrLib.Models.Entities;
using BossrScraper.Models.Entities;
using System.Collections.Generic;

namespace BossrScraper.Services.Comparers
{
    public interface ICreatureComparer
    {
        IEnumerable<ICreature> FindMissingCreatures(IEnumerable<IStatistic> statistics, IEnumerable<ICreature> existingCreatures);
    }
}
