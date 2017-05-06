using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface ISpawnRepository : ICrudable<ISpawn>
    {
        Task<IEnumerable<ISpawn>> ReadAllByWorldIdAsync(int worldId);
    }
}