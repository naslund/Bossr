using BossrApi.Models.Entities;
using BossrApi.Repositories.Interfaces;

namespace BossrApi.Repositories
{
    public interface ICreatureRepository : 
        ICrudable<ICreature>,
        IListableByIsMonitored<ICreature>, 
        IReadableByName<ICreature>
    {
    }
}