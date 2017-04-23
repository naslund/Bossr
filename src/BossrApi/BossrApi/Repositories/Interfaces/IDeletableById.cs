using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface IDeletableById
    {
        Task DeleteByIdAsync(int id);
    }
}
