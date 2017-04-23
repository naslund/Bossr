namespace BossrApi.Models.Entities
{
    public interface ICreature
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsMonitored { get; set; }
    }
}