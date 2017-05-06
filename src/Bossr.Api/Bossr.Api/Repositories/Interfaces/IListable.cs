using Bossr.Lib.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories.Interfaces
{
    public interface IListable<T> where T : IEntity
    {
        Task<IEnumerable<T>> ReadAllAsync();
    }
}
