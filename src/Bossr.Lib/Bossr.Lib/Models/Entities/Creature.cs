using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface ICreature : IEntity, INameable, IMonitorable
    {
        IEnumerable<ISpawn> Spawns { get; set; }
        IEnumerable<IStatistic> Statistics { get; set; }
        IEnumerable<ITag> Tags { get; set; }
    }

    public class Creature : ICreature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMonitored { get; set; }

        public IEnumerable<ISpawn> Spawns { get; set; }
        public IEnumerable<IStatistic> Statistics { get; set; }
        public IEnumerable<ITag> Tags { get; set; }
    }
}