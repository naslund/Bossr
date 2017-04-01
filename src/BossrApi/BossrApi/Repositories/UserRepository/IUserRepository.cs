using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IUser>> ReadAsync();
        Task<IUser> ReadAsync(string username);
        Task<IUser> ReadAsync(int id);
        Task CreateAsync(string username, string password);
        Task UpdatePasswordAsync(string id, string password);
        Task DeleteAsync(int id);
    }
}
