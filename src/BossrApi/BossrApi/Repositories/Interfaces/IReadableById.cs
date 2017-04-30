using BossrLib.Models.Interfaces;
using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface IReadableById<T> where T : IEntity
    {
        Task<T> ReadByIdAsync(int id);
    }
}
