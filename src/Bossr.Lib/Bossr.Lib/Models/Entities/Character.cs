using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface ICharacter : IEntity, INameable
    {
        int WorldId { get; set; }
        int? RaidId { get; set; }
        int UserId { get; set; }

        User User { get; set; }
        World World { get; set; }
        Raid Raid { get; set; }
    }

    public class Character : ICharacter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int WorldId { get; set; }
        public int? RaidId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public World World { get; set; }
        public Raid Raid { get; set; }
    }
}
