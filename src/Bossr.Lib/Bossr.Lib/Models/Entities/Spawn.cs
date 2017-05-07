using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    interface ISpawn : IEntity { }

    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public int Amount { get; set; }
    }
}
