using Bossr.Api.Converters;
using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Bossr.Api.Mappers
{
    public interface IScrapeMapper
    {
        void MapRelations(IEnumerable<IScrape> scrapes, IEnumerable<IStatistic> statistics);
        ScrapeDto MapToScrapeDto(IScrape scrape);
        IScrape MapToScrape(ScrapeDto scrapeDto);
    }

    public class ScrapeMapper : IScrapeMapper
    {
        public ScrapeDto MapToScrapeDto(IScrape scrape)
        {
            return new ScrapeDto
            {
                Id = scrape.Id,
                Date = LocalDateStringConverter.ToString(scrape.Date),
                Statistics = scrape.Statistics
            };
        }

        public IScrape MapToScrape(ScrapeDto scrapeDto)
        {
            return new Scrape
            {
                Id = scrapeDto.Id,
                Date = LocalDateStringConverter.ToLocalDate(scrapeDto.Date),
                Statistics = scrapeDto.Statistics
            };
        }

        public void MapRelations(IEnumerable<IScrape> scrapes, IEnumerable<IStatistic> statistics)
        {
            foreach (var scrape in scrapes)
            {
                scrape.Statistics = statistics.Where(x => x.ScrapeId == scrape.Id);
            }
        }
    }
}
