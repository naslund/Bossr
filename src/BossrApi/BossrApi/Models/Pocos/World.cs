using BossrApi.Models.Interfaces;

namespace BossrApi.Models.Pocos
{
    public class World : IEntity, IWorld
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
