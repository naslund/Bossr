using NodaTime;

namespace BossrApi.Models.Entities
{
    public interface IScrape
    {
        int Id { get; set; }
        LocalDate Date { get; set; }
    }
}
