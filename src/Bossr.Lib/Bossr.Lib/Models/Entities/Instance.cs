using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IInstance : IEntity
    {
        int FrequencyHoursMin { get; set; }
        int FrequencyHoursMax { get; set; }
        int CreatureId { get; set; }
        int? PositionId { get; set; }
    }

    public class Instance : IInstance
    {
        public int Id { get; set; }
        public int FrequencyHoursMin { get; set; }
        public int FrequencyHoursMax { get; set; }
        public int CreatureId { get; set; }
        public int? PositionId { get; set; }
    }
}
