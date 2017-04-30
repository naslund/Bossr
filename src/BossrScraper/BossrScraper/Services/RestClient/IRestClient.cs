using BossrLib.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public interface IRestClient
    {
        Task<IEnumerable<IWorld>> GetWorldsAsync();

        Task<IEnumerable<ICreature>> GetCreaturesAsync();

        Task<ScrapeDto> GetLatestScrapeAsync();

        Task PostWorldAsync(IWorld world);

        Task PostCreatureAsync(ICreature creature);

        Task PostScrapeAsync(ScrapeDto scrape);

        Task PostSpawnAsync(ISpawn spawn);
    }
}