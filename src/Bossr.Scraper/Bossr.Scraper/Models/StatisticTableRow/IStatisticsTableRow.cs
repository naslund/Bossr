namespace Bossr.Scraper.Models.Entities
{
    public interface IStatisticsTableRow
    {
        int WorldId { get; set; }
        string CreatureName { get; set; }
        int CreaturesKilled { get; set; }
        int PlayersKilled { get; set; }
    }
}
