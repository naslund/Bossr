using BossrApi.Repositories.Interfaces;
using BossrLib.Models.Entities;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IScrapeRepository : ICrudable<IScrape>
    {
        Task<IScrape> ReadLatest();
    }
}
