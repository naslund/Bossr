using BossrLib.Models.Interfaces;
using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface IUpdatable<T> where T : IEntity
    {
        Task UpdateAsync(T entity);
    }
}
