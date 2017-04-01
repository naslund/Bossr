namespace BossrApi.Models.Interfaces
{
    public interface IUser
    {
        string Username { get; set; }
        string HashedPassword { get; set; }
        string Salt { get; set; }
    }
}
