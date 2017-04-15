using BossrScraper.Models.Entities;
using BossrScraper.Models.ScrapeItems;
using System.Collections.Generic;

namespace BossrScraper.Services.Comparers.WorldComparer
{
    public interface IWorldComparer
    {
        IEnumerable<IWorld> FindMissingWorlds(IEnumerable<IWorld> scrapeWorlds, IEnumerable<IWorld> existingWorlds);
    }
}
