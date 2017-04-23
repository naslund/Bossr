using BossrApi.Models.Interfaces;

namespace BossrApi.Repositories.Interfaces
{
    public interface ICrudable<T> : 
        ICreateable<T>,
        IDeletableById,
        IListable<T>,
        IReadableById<T>,
        IUpdatable<T>
        where T : IEntity 
    {
    }
}
