using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services.Parsers
{
    public interface IStatisticParser
    {
        Task<IEnumerable<IStatistic>> Parse(HttpResponseMessage response, int worldId);
    }
}
