using Bossr.Lib.Models.Interfaces;
using NodaTime;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface IScrape : IEntity
    {
        LocalDate Date { get; set; }

        IEnumerable<IStatistic> Statistics { get; set; }
    }

    public class Scrape : IScrape
    {
        public int Id { get; set; }
        public LocalDate Date { get; set; }

        public IEnumerable<IStatistic> Statistics { get; set; }
    }

    public class ScrapeDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
    }
}
