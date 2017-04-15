using BossrScraper.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrScraper.Services.RestClient
{
    public interface IRestClient
    {
        Task<IEnumerable<IWorld>> GetWorldsAsync();
        Task PostWorldAsync(IWorld world);
    }
}
