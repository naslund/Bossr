using BossrApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IPositionRepository
    {
        Task CreateAsync(IPosition position);

        Task DeleteAsync(int id);

        Task<IEnumerable<IPosition>> ReadAllAsync();

        Task<IPosition> ReadAsync(int id);

        Task UpdateAsync(IPosition position);
    }
}