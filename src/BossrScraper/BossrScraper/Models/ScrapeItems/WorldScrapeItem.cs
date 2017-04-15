namespace BossrScraper.Models.ScrapeItems
{
    public class WorldScrapeItem : IWorldScrapeItem
    {
        public string Name { get; set; }
        public string PlayersOnline { get; set; }
        public string Location { get; set; }
        public string PvpType { get; set; }
        public string Tags { get; set; }
    }
}