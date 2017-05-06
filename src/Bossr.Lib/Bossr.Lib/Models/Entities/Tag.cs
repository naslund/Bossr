using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface ITag : IEntity, INameable
    {
        int CategoryId { get; set; }
    }

    public class Tag : ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
