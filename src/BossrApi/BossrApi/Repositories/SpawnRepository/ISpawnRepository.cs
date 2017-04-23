using BossrApi.Models.Entities;
using BossrApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface ISpawnRepository : ICrudable<ISpawn>
    {
        Task<IEnumerable<ISpawn>> ReadAllByWorldIdAsync(int worldId);
    }
}