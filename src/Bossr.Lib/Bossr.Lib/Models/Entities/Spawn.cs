using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface ISpawn : IEntity
    {
        int CreatureId { get; set; }
        int WorldId { get; set; }
        int ScrapeId { get; set; }
        int Amount { get; set; }
    }

    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public int WorldId { get; set; }
        public int ScrapeId { get; set; }
        public int Amount { get; set; }
    }
}