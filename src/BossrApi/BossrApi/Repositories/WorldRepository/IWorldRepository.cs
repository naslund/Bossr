using BossrApi.Models.Entities;
using BossrApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public interface IWorldRepository :
        ICrudable<IWorld>,
        IListableByIsMonitored<IWorld>,
        IReadableByName<IWorld>
    {
    }
}