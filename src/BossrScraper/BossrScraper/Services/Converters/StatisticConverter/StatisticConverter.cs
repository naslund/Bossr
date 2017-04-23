using System;
using System.Collections.Generic;
using BossrScraper.Models.Entities;
using System.Linq;

namespace BossrScraper.Services.Converters
{
    public class StatisticConverter : IStatisticConverter
    {
        public IEnumerable<ISpawn> ConvertToSpawns(IEnumerable<IStatistic> statistics, IEnumerable<IWorld> worlds, IEnumerable<ICreature> creatures, IScrapeDto scrapeDto)
        {
            return statistics
                .Where(x => x.CreaturesKilled > 0 || x.PlayersKilled > 0)
                .Where(x => creatures.Single(y => y.Name == x.CreatureName).IsMonitored)
                .Where(x => worlds.Single(y => y.Id == x.WorldId).IsMonitored)
                .Select(x => new Spawn
                {
                    WorldId = x.WorldId,
                    ScrapeId = scrapeDto.Id,
                    CreatureId = creatures.Single(y => y.Name == x.CreatureName).Id,
                    Amount = x.CreaturesKilled == 0 ? 1 : x.CreaturesKilled
                });
        }
    }
}
