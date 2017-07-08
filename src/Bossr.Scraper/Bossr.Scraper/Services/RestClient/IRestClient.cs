using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public interface IRestClient
    {
        Task<IEnumerable<IWorld>> GetWorldsAsync();

        Task<IEnumerable<ICreature>> GetCreaturesAsync();

        Task<Scrape> GetLatestScrapeAsync();

        Task PostWorldAsync(IWorld world);

        Task PostCreatureAsync(ICreature creature);

        Task PostScrapeAsync(Scrape scrape);

        Task PostStatisticAsync(IStatistic statistic);
    }
}