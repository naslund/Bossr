using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.ScrapeRepository
{
    public interface IScrapeRepository
    {
        Task CreateAsync(IScrape scrape);

        Task DeleteAsync(int id);

        Task<IEnumerable<IScrape>> ReadAllAsync();

        Task<IScrape> ReadAsync(int id);

        Task UpdateAsync(IScrape scrape);
    }
}
