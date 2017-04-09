using BossrApi.Models.Interfaces;
using System;

namespace BossrApi.Models.Entities
{
    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public int WorldId { get; set; }
        public DateTimeOffset TimeMin { get; set; }
        public DateTimeOffset TimeMax { get; set; }
    }
}