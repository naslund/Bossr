using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface IWorld : IEntity, INameable, IMonitorable
    {
        IEnumerable<IStatistic> Statistics { get; set; }
        IEnumerable<ITag> Tags { get; set; }
    }

    public class World : IWorld
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMonitored { get; set; }

        public IEnumerable<IStatistic> Statistics { get; set; }
        public IEnumerable<ITag> Tags { get; set; }
    }
}