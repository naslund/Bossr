using Bossr.Lib.Models.Interfaces;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface IUpdatable<T> where T : IEntity
    {
        Task UpdateAsync(T entity);
    }
}
