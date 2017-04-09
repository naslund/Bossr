using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task CreateAsync(IUser user);

        Task DeleteAsync(int id);

        Task<IEnumerable<IUser>> ReadAsync();

        Task<IUser> ReadAsync(string username);

        Task<IUser> ReadAsync(int id);

        Task UpdateAsync(IUser user);
    }
}