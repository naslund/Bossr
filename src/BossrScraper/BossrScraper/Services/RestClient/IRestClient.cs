using BossrScraper.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public interface IRestClient
    {
        Task<IEnumerable<IWorld>> GetWorldsAsync();

        Task<IEnumerable<ICreature>> GetCreaturesAsync();

        Task<IScrapeDto> GetLatestScrapeAsync();

        Task PostWorldAsync(IWorld world);

        Task PostCreatureAsync(ICreature creature);

        Task PostScrapeAsync(IScrapeDto scrape);

        Task PostSpawnAsync(ISpawn spawn);
    }
}