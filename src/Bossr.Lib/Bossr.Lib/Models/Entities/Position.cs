using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IPosition : IEntity, INameable
    {
        Coordinates Coordinates { get; set; }
    }

    public class Position : IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }
    }

    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}