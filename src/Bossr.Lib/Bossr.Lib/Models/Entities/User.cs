using Bossr.Lib.Models.Interfaces;

namespace Bossr.Lib.Models.Entities
{
    public interface IUser : IEntity
    {
        string Username { get; set; }
        string HashedPassword { get; set; }
        string Salt { get; set; }
    }

    public class User : IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}