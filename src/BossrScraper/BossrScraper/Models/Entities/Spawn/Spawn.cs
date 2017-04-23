namespace BossrScraper.Models.Entities
{
    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public int CreatureId { get; set; }
        public int ScrapeId { get; set; }
    }
}
