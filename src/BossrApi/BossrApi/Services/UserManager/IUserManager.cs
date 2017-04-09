using System.Threading.Tasks;

namespace BossrApi.Services.UserManager
{
    public interface IUserManager
    {
        Task CreateUserAsync(string username, string password);

        Task UpdatePasswordAsync(int id, string password);
    }
}