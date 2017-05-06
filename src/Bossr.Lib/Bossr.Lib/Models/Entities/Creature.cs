using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface ICreature : IEntity, INameable, IMonitorable { }

    public class Creature : ICreature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMonitored { get; set; }
    }
}