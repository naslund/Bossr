using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IUser : IEntity
    {
        string Username { get; set; }
        string HashedPassword { get; set; }
        string Salt { get; set; }
    }
}