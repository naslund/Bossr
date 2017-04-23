using BossrApi.Models.Entities;
using BossrApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IScrapeRepository : ICrudable<IScrape>
    {
        Task<IScrape> ReadLatest();
    }
}
