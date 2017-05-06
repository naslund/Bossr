using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;

namespace Bossr.Scraper.Services.Comparers
{
    public interface IWorldComparer
    {
        IEnumerable<IWorld> FindMissingWorlds(IEnumerable<IWorld> scrapeWorlds, IEnumerable<IWorld> existingWorlds);
    }
}
