using BossrApi.Models.Interfaces;

namespace BossrApi.Models.Entities
{
    public interface ITag : IEntity, INameable
    {
        int CategoryId { get; set; }
    }
}
