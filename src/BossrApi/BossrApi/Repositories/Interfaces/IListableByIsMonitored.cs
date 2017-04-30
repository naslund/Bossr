using BossrLib.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.Interfaces
{
    public interface IListableByIsMonitored<T> where T : IMonitorable
    {
        Task<IEnumerable<T>> ReadAllByIsMonitoredAsync(bool isMonitored);
    }
}
