using Bossr.Lib.Models.Interfaces;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface ICreateable<T> where T : IEntity
    {
        Task CreateAsync(T entity);
    }
}
