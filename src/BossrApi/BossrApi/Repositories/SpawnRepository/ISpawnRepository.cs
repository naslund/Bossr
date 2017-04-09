using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.SpawnRepository
{
    public interface ISpawnRepository
    {
        Task CreateAsync(ISpawn spawn);

        Task DeleteAsync(int id);

        Task<IEnumerable<ISpawn>> ReadAsync();

        Task<ISpawn> ReadAsync(int id);

        Task UpdateAsync(ISpawn spawn);
    }
}