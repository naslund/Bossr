using BossrApi.Repositories.Interfaces;
using BossrLib.Models.Entities;

namespace BossrApi.Repositories
{
    public interface IWorldRepository :
        ICrudable<IWorld>,
        IListableByIsMonitored<IWorld>,
        IReadableByName<IWorld>
    {
    }
}