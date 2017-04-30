using BossrLib.Models.Interfaces;
using NodaTime;

namespace BossrLib.Models.Entities
{
    public interface IScrape : IEntity
    {
        LocalDate Date { get; set; }
    }
}
