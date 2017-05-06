using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IPosition : IEntity, INameable
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
    }
}
