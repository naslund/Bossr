using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface IDeletableById
    {
        Task DeleteByIdAsync(int id);
    }
}
