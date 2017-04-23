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
            var spawns = new List<ISpawn>();
            var currentStats = statistics
                .Where(x => x.CreaturesKilled > 0 || x.PlayersKilled > 0)
                .Where(x => creatures.Single(y => y.Name == x.CreatureName).IsMonitored)
                .Where(x => worlds.Single(y => y.Id == x.WorldId).IsMonitored)
                .ToList();

            foreach (var stat in currentStats)
            {
                if (stat.CreaturesKilled == 0) stat.CreaturesKilled = 1;
                while (stat.CreaturesKilled > 0)
                {
                    spawns.Add(new Spawn
                    {
                        WorldId = stat.WorldId,
                        CreatureId = creatures.Single(x => x.Name == stat.CreatureName).Id,
                        ScrapeId = scrapeDto.Id
                    });
                    stat.CreaturesKilled--;
                }
            }

            return spawns;
        }
    }
}
