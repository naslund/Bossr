namespace BossrLib.Models.Entities
{
    public class Instance : IInstance
    {
        public int Id { get; set; }
        public int FrequencyHoursMin { get; set; }
        public int FrequencyHoursMax { get; set; }
        public int CreatureId { get; set; }
        public int? PositionId { get; set; }
    }
}
