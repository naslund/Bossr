using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface ICategory : IEntity, INameable { }

    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
