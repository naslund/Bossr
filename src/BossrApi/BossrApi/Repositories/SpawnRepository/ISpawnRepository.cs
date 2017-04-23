using BossrApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface ISpawnRepository
    {
        Task CreateAsync(ISpawn spawn);

        Task DeleteAsync(int id);

        Task<IEnumerable<ISpawn>> ReadAllAsync();

        Task<ISpawn> ReadAsync(int id);

        Task UpdateAsync(ISpawn spawn);
    }
}