using System;

namespace BossrApi.Models.Interfaces
{
    public interface ISpawn
    {
        int Id { get; set; }
        int CreatureId { get; set; }
        int WorldId { get; set; }
        DateTime TimeMinUtc { get; set; }
        DateTime TimeMaxUtc { get; set; }
    }
}