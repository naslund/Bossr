namespace Bossr.Lib.Models.Entities
{
    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public int WorldId { get; set; }
        public int ScrapeId { get; set; }
        public int Amount { get; set; }
    }
}