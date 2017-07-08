namespace Bossr.Scraper.Models.Entities
{
    public interface IStatisticsTableRow
    {
        int WorldId { get; set; }
        string CreatureName { get; set; }
        int CreaturesKilled { get; set; }
        int PlayersKilled { get; set; }
    }

    public class StatisticsTableRow : IStatisticsTableRow
    {
        public int WorldId { get; set; }
        public string CreatureName { get; set; }
        public int CreaturesKilled { get; set; }
        public int PlayersKilled { get; set; }
    }
}
