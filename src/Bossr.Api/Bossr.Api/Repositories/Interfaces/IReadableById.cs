using Bossr.Lib.Models.Interfaces;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface IReadableById<T> where T : IEntity
    {
        Task<T> ReadByIdAsync(int id);
    }
}
