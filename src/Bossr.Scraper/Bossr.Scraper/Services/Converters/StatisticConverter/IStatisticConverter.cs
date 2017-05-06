using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;

namespace Bossr.Scraper.Services.Converters
{
    public interface IStatisticConverter
    {
        IEnumerable<ISpawn> ConvertToSpawns(IEnumerable<IStatistic> statistics, IEnumerable<IWorld> worlds, IEnumerable<ICreature> creatures, ScrapeDto scrapeDto);
    }
}
