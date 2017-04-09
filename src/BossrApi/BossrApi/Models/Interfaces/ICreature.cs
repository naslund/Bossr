namespace BossrApi.Models.Interfaces
{
    public interface ICreature
    {
        int Id { get; set; }
        string Name { get; set; }
        int SpawnRateHoursMin { get; set; }
        int SpawnRateHoursMax { get; set; }
        bool IsMonitored { get; set; }
    }
}