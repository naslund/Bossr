using BossrApi.Models.Interfaces;
using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface ICreateable<T> where T : IEntity
    {
        Task CreateAsync(T entity);
    }
}
