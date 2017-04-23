using BossrApi.Models.Interfaces;
using NodaTime;

namespace BossrApi.Models.Entities
{
    public interface IScrape : IEntity
    {
        LocalDate Date { get; set; }
    }
}
