using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Models.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services.Parsers
{
    public interface IWorldParser
    {
        Task<IEnumerable<IWorld>> Parse(HttpResponseMessage response);
    }
}