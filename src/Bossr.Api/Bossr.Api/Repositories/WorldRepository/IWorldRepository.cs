using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;

namespace Bossr.Api.Repositories
{
    public interface IWorldRepository :
        ICrudable<IWorld>,
        IListableByIsMonitored<IWorld>,
        IReadableByName<IWorld>
    {
    }
}