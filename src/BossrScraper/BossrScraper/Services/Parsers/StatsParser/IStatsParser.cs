using BossrScraper.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.Parsers
{
    public interface IStatsParser
    {
        Task<IEnumerable<IStatistic>> Parse(HttpResponseMessage response, int worldId);
    }
}
