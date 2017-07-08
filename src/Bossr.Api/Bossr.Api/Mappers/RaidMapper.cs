using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Api.Mappers
{
    public interface IRaidMapper
    {
        void MapRelations(IEnumerable<IRaid> raids, IEnumerable<ISpawn> spawns, IEnumerable<ICreature> creatures, IEnumerable<IPosition> positions);
    }

    public class RaidMapper : IRaidMapper
    {
        public void MapRelations(IEnumerable<IRaid> raids, IEnumerable<ISpawn> spawns, IEnumerable<ICreature> creatures, IEnumerable<IPosition> positions)
        {
            foreach (var spawn in spawns)
            {
                spawn.Creature = creatures.Single(x => x.Id == spawn.CreatureId);
                spawn.Positions = positions.Where(x => x.SpawnId == spawn.Id);
            }

            foreach (var raid in raids)
            {
                raid.Spawns = spawns.Where(x => x.RaidId == raid.Id);
            }
        }
    }
}
