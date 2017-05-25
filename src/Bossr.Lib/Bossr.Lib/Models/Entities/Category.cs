using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface ICategory : IEntity, INameable
    {
        IEnumerable<ITag> Tags { get; set; }
    }

    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ITag> Tags { get; set; }
    }
}