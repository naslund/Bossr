using Bossr.Lib.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Bossr.Lib.Models.Entities
{
    public interface IScrape : IEntity
    {
        DateTime Date { get; set; }

        IEnumerable<IStatistic> Statistics { get; set; }
    }

    public class Scrape : IScrape
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<IStatistic> Statistics { get; set; }
    }
}
