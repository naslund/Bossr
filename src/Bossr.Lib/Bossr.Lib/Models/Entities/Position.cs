﻿using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IPosition : IEntity, INameable
    {
        int X { get; set; }
        int Y { get; set; }
        int Z { get; set; }
    }

    public class Position : IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}