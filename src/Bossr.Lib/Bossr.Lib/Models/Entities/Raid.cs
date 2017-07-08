using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface IRaid : IEntity
    {
        int FrequencyHoursMin { get; set; }
        int FrequencyHoursMax { get; set; }
        IEnumerable<ISpawn> Spawns { get; set; }
        IEnumerable<ITag> Tags { get; set; }
    }

    public class Raid : IRaid
    {
        public int Id { get; set; }
        public int FrequencyHoursMin { get; set; }
        public int FrequencyHoursMax { get; set; }

        public IEnumerable<ISpawn> Spawns { get; set; }
        public IEnumerable<ITag> Tags { get; set; }
    }
}
