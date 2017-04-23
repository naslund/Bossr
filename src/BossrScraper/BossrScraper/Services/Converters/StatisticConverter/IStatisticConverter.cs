using BossrScraper.Models.Entities;
using System.Collections.Generic;

namespace BossrScraper.Services.Converters
{
    public interface IStatisticConverter
    {
        IEnumerable<ISpawn> ConvertToSpawns(IEnumerable<IStatistic> statistics, IEnumerable<IWorld> worlds, IEnumerable<ICreature> creatures, IScrapeDto scrapeDto);
    }
}
