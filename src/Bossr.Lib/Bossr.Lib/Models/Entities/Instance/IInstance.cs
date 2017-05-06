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
}
