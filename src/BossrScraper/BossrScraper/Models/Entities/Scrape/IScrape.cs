using NodaTime;
using System;

namespace BossrScraper.Models.Entities
{
    public interface IScrape
    {
        int Id { get; set; }
        LocalDate Date { get; set; }
    }
}
