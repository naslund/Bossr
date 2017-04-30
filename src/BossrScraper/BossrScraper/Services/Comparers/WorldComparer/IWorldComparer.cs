using BossrLib.Models.Entities;
using BossrScraper.Models.Entities;
using System.Collections.Generic;

namespace BossrScraper.Services.Comparers
{
    public interface IWorldComparer
    {
        IEnumerable<IWorld> FindMissingWorlds(IEnumerable<IWorld> scrapeWorlds, IEnumerable<IWorld> existingWorlds);
    }
}
