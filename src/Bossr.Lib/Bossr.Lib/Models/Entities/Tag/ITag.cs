using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface ITag : IEntity, INameable
    {
        int CategoryId { get; set; }
    }
}
