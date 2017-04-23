namespace BossrScraper.Models.Entities
{
    public interface ISpawn
    {
        int Id { get; set; }
        int WorldId { get; set; }
        int CreatureId { get; set; }
        int ScrapeId { get; set; }
    }
}
