using BossrApi.Models.Entities;
using BossrApi.Repositories.Interfaces;

namespace BossrApi.Repositories
{
    public interface ICategoryRepository :
        ICrudable<ICategory>
    {
    }
}
