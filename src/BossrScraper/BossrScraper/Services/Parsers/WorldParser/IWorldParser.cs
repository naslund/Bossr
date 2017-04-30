using BossrLib.Models.Entities;
using BossrScraper.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.Parsers
{
    public interface IWorldParser
    {
        Task<IEnumerable<IWorld>> Parse(HttpResponseMessage response);
    }
}