using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Scraper.Services.Comparers
{
    public class WorldComparer : IWorldComparer
    {
        public IEnumerable<IWorld> FindMissingWorlds(IEnumerable<IWorld> scrapeWorlds, IEnumerable<IWorld> existingWorlds)
        {
            return scrapeWorlds.Where(x => !existingWorlds.Any(y => y.Name == x.Name));
        }
    }
}
