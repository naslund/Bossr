using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Parsers
{
    public interface IStatisticsTableRowParser
    {
        Task<IEnumerable<IStatisticsTableRow>> Parse(HttpResponseMessage response, int worldId);
    }
}
