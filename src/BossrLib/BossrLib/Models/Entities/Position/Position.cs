namespace BossrLib.Models.Entities
{
    public class Position : IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int RespawnHoursMin { get; set; }
        public int RespawnHoursMax { get; set; }
    }
}