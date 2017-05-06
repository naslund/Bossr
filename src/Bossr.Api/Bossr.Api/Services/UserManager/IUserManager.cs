using System.Threading.Tasks;

namespace Bossr.Api.Services
{
    public interface IUserManager
    {
        Task CreateUserAsync(string username, string password);

        Task UpdatePasswordAsync(int id, string password);
    }
}