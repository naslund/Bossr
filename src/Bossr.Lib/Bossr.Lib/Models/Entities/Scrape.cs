using Bossr.Lib.Models.Interfaces;
using NodaTime;

namespace Bossr.Lib.Models.Entities
{
    public interface IScrape : IEntity
    {
        LocalDate Date { get; set; }
    }

    public class Scrape : IScrape
    {
        public int Id { get; set; }
        public LocalDate Date { get; set; }
    }

    public class ScrapeDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
    }
}
