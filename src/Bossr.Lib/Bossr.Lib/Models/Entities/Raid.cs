using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IRaid : IEntity { }

    public class Raid : IRaid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FrequencyHoursMin { get; set; }
        public int FrequencyHoursMax { get; set; }
    }
}
