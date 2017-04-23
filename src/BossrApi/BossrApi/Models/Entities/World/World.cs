namespace BossrApi.Models.Entities
{
    public class World : IWorld
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMonitored { get; set; }
    }
}