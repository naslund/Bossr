using BossrApi.Models.Entities;
using BossrApi.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IUserRepository : ICrudable<IUser>
    {
        Task<IUser> ReadByUsername(string username);
    }
}