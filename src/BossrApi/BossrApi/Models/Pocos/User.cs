using BossrApi.Models.Interfaces;

namespace BossrApi.Models.Pocos
{
    public class User : IEntity, IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
