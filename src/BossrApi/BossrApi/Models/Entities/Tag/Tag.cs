namespace BossrApi.Models.Entities
{
    public class Tag : ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
