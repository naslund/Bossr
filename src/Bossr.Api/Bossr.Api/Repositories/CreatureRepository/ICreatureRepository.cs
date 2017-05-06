using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;

namespace Bossr.Api.Repositories
{
    public interface ICreatureRepository : 
        ICrudable<ICreature>,
        IListableByIsMonitored<ICreature>, 
        IReadableByName<ICreature>
    {
    }
}