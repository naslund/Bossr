using Bossr.Lib.Models.Interfaces;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface IDeletableById<T> where T : IEntity
    {
        Task DeleteByIdAsync(int id);
    }
}
