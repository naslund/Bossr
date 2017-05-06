using Bossr.Lib.Models.Interfaces;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface IReadableByName<T> where T : INameable
    {
        Task<T> ReadByNameAsync(string name);
    }
}
