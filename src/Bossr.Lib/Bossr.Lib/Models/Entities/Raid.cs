using Bossr.Lib.Models.Interfaces;
using NodaTime;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface IRaid : IEntity, INameable
    {
        Duration FrequencyMin { get; set; }
        Duration FrequencyMax { get; set; }

        IEnumerable<ISpawn> Spawns { get; set; }
        IEnumerable<ITag> Tags { get; set; }
    }

    public class Raid : IRaid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Duration FrequencyMin { get; set; }
        public Duration FrequencyMax { get; set; }

        public IEnumerable<ISpawn> Spawns { get; set; }
        public IEnumerable<ITag> Tags { get; set; }
    }

    public class RaidDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FrequencyHoursMin { get; set; }
        public int FrequencyHoursMax { get; set; }

        public IEnumerable<ISpawn> Spawns { get; set; }
        public IEnumerable<ITag> Tags { get; set; }
    }
}
