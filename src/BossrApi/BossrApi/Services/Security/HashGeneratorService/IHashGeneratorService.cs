namespace BossrApi.Services.Security.HashGeneratorService
{
    public interface IHashGeneratorService
    {
        string GenerateSaltedHash(string password, string salt);
    }
}
