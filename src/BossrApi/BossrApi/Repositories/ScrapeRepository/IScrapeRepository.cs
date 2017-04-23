using BossrApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IScrapeRepository
    {
        Task CreateAsync(IScrape scrape);

        Task DeleteAsync(int id);

        Task<IEnumerable<IScrape>> ReadAllAsync();

        Task<IScrape> ReadAsync(int id);

        Task<IScrape> ReadLatest();

        Task UpdateAsync(IScrape scrape);
    }
}
