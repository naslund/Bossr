using BossrApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface ICityRepository
    {
        Task CreateAsync(ICity city);

        Task DeleteAsync(int id);

        Task<IEnumerable<ICity>> ReadAllAsync();

        Task<ICity> ReadAsync(int id);

        Task UpdateAsync(ICity city);
    }
}