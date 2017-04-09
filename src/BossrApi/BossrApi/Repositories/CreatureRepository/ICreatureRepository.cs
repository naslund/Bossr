using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.CreatureRepository
{
    public interface ICreatureRepository
    {
        Task CreateAsync(ICreature creature);

        Task DeleteAsync(int id);

        Task<ICreature> ReadAsync(int id);

        Task<ICreature> ReadAsync(string name);

        Task<IEnumerable<ICreature>> ReadAsync();

        Task UpdateAsync(ICreature creature);
    }
}