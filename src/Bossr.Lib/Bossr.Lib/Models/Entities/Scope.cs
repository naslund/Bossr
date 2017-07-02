using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IScope : IEntity, INameable { }

    public class Scope : IScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
