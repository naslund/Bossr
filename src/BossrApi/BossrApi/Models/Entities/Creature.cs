using BossrApi.Models.Interfaces;

namespace BossrApi.Models.Entities
{
    public class Creature : ICreature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpawnRateHoursMin { get; set; }
        public int SpawnRateHoursMax { get; set; }
        public bool IsMonitored { get; set; }
    }
}