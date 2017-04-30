using BossrLib.Models.Interfaces;

namespace BossrLib.Models.Entities
{
    public interface ISpawn : IEntity
    {
        int CreatureId { get; set; }
        int WorldId { get; set; }
        int ScrapeId { get; set; }
        int Amount { get; set; }
    }
}