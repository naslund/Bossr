using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;

namespace Bossr.Scraper.Services.Comparers
{
    public interface ICreatureComparer
    {
        IEnumerable<ICreature> FindMissingCreatures(IEnumerable<IStatistic> statistics, IEnumerable<ICreature> existingCreatures);
    }
}
