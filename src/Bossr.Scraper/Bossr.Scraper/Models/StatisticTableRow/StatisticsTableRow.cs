namespace Bossr.Scraper.Models.Entities
{
    public class StatisticsTableRow : IStatisticsTableRow
    {
        public int WorldId { get; set; }
        public string CreatureName { get; set; }
        public int CreaturesKilled { get; set; }
        public int PlayersKilled { get; set; }
    }
}
