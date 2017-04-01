using BossrApi.Models;
using BossrApi.Models.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossrApi.Repositories.WorldRepository
{
    interface IWorldRepository
    {
        Task<IEnumerable<World>> ReadAsync();
        Task<World> ReadAsync(int id);
        Task<World> ReadAsync(string name);
        Task CreateAsync(string name);
        Task UpdateNameAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
