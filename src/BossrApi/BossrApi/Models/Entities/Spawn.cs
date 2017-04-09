using BossrApi.Models.Interfaces;
using System;

namespace BossrApi.Models.Entities
{
    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public int WorldId { get; set; }
        public DateTime TimeMinUtc { get; set; }
        public DateTime TimeMaxUtc { get; set; }
    }
}