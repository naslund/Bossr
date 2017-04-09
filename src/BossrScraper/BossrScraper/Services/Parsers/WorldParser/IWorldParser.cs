using BossrScraper.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.Parsers.WorldParser
{
    public interface IWorldParser
    {
        Task<IEnumerable<IWorld>> Parse(HttpResponseMessage response);
    }
}