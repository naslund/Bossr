using BossrApi.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface IListable<T> where T : IEntity
    {
        Task<IEnumerable<T>> ReadAllAsync();
    }
}
