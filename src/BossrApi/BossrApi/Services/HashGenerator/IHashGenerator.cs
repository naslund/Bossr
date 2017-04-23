namespace BossrApi.Services
{
    public interface IHashGenerator
    {
        string GenerateSaltedHash(string password, string salt);
    }
}