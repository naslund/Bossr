using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface ITag : IEntity, INameable
    {
        int CategoryId { get; set; }

        Category Category { get; set; }
        IEnumerable<IRaid> Raids { get; set; }
        IEnumerable<ICreature> Creatures { get; set; }
        IEnumerable<IWorld> Worlds { get; set; }
        IEnumerable<IPosition> Positions { get; set; }
    }

    public class Tag : ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public IEnumerable<IRaid> Raids { get; set; }
        public IEnumerable<ICreature> Creatures { get; set; }
        public IEnumerable<IWorld> Worlds { get; set; }
        public IEnumerable<IPosition> Positions { get; set; }
    }
}
