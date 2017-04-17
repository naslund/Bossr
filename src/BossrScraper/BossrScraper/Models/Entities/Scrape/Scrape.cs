using NodaTime;

namespace BossrScraper.Models.Entities
{
    public class Scrape : IScrape
    {
        public int Id { get; set; }
        public LocalDate Date { get; set; }
    }
}
