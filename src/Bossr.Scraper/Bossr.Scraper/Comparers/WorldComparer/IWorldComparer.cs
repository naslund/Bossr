using Bossr.Lib.Models.Entities;
using System.Collections.Generic;

namespace Bossr.Scraper.Comparers
{
    public interface IWorldComparer
    {
        IEnumerable<IWorld> FindMissingWorlds(IEnumerable<IWorld> scrapeWorlds, IEnumerable<IWorld> existingWorlds);
    }
}
