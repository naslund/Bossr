namespace BossrScraper.Models.Entities
{
    public class Statistic : IStatistic
    {
        public int WorldId { get; set; }
        public string CreatureName { get; set; }
        public int CreaturesKilled { get; set; }
        public int PlayersKilled { get; set; }
    }
}
