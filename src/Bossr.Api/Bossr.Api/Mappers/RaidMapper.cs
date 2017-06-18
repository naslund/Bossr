using Bossr.Api.Converters;
using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Api.Mappers
{
    public interface IRaidMapper
    {
        void MapRelations(IEnumerable<IRaid> raids, IEnumerable<ISpawn> spawns, IEnumerable<ICreature> creatures, IEnumerable<IPosition> positions);
        RaidDto MapToRaidDto(IRaid raid);
        IRaid MapToRaid(RaidDto raidDto);
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

        public RaidDto MapToRaidDto(IRaid raid)
        {
            return new RaidDto
            {
                Id = raid.Id,
                Name = raid.Name,
                FrequencyHoursMin = DurationHoursConverter.ToHours(raid.FrequencyMin),
                FrequencyHoursMax = DurationHoursConverter.ToHours(raid.FrequencyMax),
                Spawns = raid.Spawns,
                Tags = raid.Tags
            };
        }

        public IRaid MapToRaid(RaidDto raidDto)
        {
            return new Raid
            {
                Id = raidDto.Id,
                Name = raidDto.Name,
                FrequencyMin = DurationHoursConverter.ToDuration(raidDto.FrequencyHoursMin),
                FrequencyMax = DurationHoursConverter.ToDuration(raidDto.FrequencyHoursMax),
                Spawns = raidDto.Spawns,
                Tags = raidDto.Tags
            };
        }
    }
}
