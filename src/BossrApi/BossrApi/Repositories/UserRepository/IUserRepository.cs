using BossrApi.Repositories.Interfaces;
using BossrLib.Models.Entities;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IUserRepository : ICrudable<IUser>
    {
        Task<IUser> ReadByUsername(string username);
    }
}