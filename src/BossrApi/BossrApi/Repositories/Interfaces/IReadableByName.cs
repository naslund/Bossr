using BossrLib.Models.Interfaces;
using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface IReadableByName<T> where T : INameable
    {
        Task<T> ReadByNameAsync(string name);
    }
}
