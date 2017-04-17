using BossrScraper.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public interface IRestClient
    {
        Task<IEnumerable<IWorld>> GetWorldsAsync();

        Task<IEnumerable<ICreature>> GetCreaturesAsync();

        Task PostWorldAsync(IWorld world);

        Task PostCreatureAsync(ICreature creature);
    }
}