using BossrLib.Models.Interfaces;

namespace BossrLib.Models.Entities
{
    public interface IPosition : IEntity, INameable
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
        int RespawnHoursMin { get; set; }
        int RespawnHoursMax { get; set; }
        int CreatureId { get; set; }
    }
}
