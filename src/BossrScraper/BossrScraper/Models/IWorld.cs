namespace BossrScraper.Models
{
    public interface IWorld
    {
        string Name { get; set; }
        string PlayersOnline { get; set; }
        string Location { get; set; }
        string PvpType { get; set; }
        string Tags { get; set; }
    }
}
