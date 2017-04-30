using BossrLib.Models.Interfaces;

namespace BossrLib.Models.Entities
{
    public interface ITag : IEntity, INameable
    {
        int CategoryId { get; set; }
    }
}
