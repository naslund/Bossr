using BossrLib.Models.Interfaces;

namespace BossrLib.Models.Entities
{
    public interface IUser : IEntity
    {
        string Username { get; set; }
        string HashedPassword { get; set; }
        string Salt { get; set; }
    }
}