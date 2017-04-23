using BossrApi.Models.Interfaces;

namespace BossrApi.Models.Entities
{
    public interface IUser : IEntity
    {
        string Username { get; set; }
        string HashedPassword { get; set; }
        string Salt { get; set; }
    }
}