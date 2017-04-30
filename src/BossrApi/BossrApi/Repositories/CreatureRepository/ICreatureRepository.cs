using BossrApi.Repositories.Interfaces;
using BossrLib.Models.Entities;

namespace BossrApi.Repositories
{
    public interface ICreatureRepository : 
        ICrudable<ICreature>,
        IListableByIsMonitored<ICreature>, 
        IReadableByName<ICreature>
    {
    }
}