using BossrScraper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public interface IWorldParser
    {
        Task<IEnumerable<IWorld>> Parse();
    }
}
