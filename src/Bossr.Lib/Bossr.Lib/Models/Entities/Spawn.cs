using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface ISpawn : IEntity
    {
        int CreatureId { get; set; }
        int RaidId { get; set; }
        int Amount { get; set; }

        IRaid Raid { get; set; }
        ICreature Creature { get; set; }
        IEnumerable<IPosition> Positions { get; set; }
    }

    public class Spawn : ISpawn
    {
        public int Id { get; set; }
        public int CreatureId { get; set; }
        public int RaidId { get; set; }
        public int Amount { get; set; }

        public IRaid Raid { get; set; }
        public ICreature Creature { get; set; }
        public IEnumerable<IPosition> Positions { get; set; }
    }
}
