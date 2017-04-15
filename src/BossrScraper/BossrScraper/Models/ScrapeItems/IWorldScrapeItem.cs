namespace BossrScraper.Models.ScrapeItems
{
    public interface IWorldScrapeItem
    {
        string Name { get; set; }
        string PlayersOnline { get; set; }
        string Location { get; set; }
        string PvpType { get; set; }
        string Tags { get; set; }
    }
}