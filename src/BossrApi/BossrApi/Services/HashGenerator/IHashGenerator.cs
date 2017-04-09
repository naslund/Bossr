namespace BossrApi.Services.HashGenerator
{
    public interface IHashGenerator
    {
        string GenerateSaltedHash(string password, string salt);
    }
}