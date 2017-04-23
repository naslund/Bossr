namespace BossrApi.Models.Entities
{
    public interface IWorld
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsMonitored { get; set; }
    }
}