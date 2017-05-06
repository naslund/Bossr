using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IWorld : IEntity, INameable, IMonitorable { }

    public class World : IWorld
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMonitored { get; set; }
    }
}