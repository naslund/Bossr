namespace BossrScraper.Models.Entities
{
    public interface IStatistic
    {
        int WorldId { get; set; }
        string CreatureName { get; set; }
        int CreaturesKilled { get; set; }
        int PlayersKilled { get; set; }
    }
}
