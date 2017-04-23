using BossrApi.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(IUser user);

        Task DeleteAsync(int id);

        Task<IEnumerable<IUser>> ReadAllAsync();

        Task<IUser> ReadAsync(string username);

        Task<IUser> ReadAsync(int id);

        Task UpdateAsync(IUser user);
    }
}