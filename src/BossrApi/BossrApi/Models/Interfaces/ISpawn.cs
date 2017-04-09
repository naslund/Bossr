using System;

namespace BossrApi.Models.Interfaces
{
    public interface ISpawn
    {
        int Id { get; set; }
        int CreatureId { get; set; }
        int WorldId { get; set; }
        DateTimeOffset TimeMin { get; set; }
        DateTimeOffset TimeMax { get; set; }
    }
}