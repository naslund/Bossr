using Bossr.Lib.Models.Interfaces;
using NodaTime;

namespace Bossr.Lib.Models.Entities
{
    public interface IScrape : IEntity
    {
        LocalDate Date { get; set; }
    }
}
