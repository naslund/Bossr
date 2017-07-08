using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Api.Mappers
{
    public interface IScrapeMapper
    {
        void MapRelations(IEnumerable<IScrape> scrapes, IEnumerable<IStatistic> statistics);
    }

    public class ScrapeMapper : IScrapeMapper
    {

        public void MapRelations(IEnumerable<IScrape> scrapes, IEnumerable<IStatistic> statistics)
        {
            foreach (var scrape in scrapes)
            {
                scrape.Statistics = statistics.Where(x => x.ScrapeId == scrape.Id);
            }
        }
    }
}
