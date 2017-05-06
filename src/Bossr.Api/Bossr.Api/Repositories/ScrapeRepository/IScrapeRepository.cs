using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface IScrapeRepository : ICrudable<IScrape>
    {
        Task<IScrape> ReadLatest();
    }
}
