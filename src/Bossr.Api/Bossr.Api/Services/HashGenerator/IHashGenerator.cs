namespace Bossr.Api.Services
{
    public interface IHashGenerator
    {
        string GenerateSaltedHash(string password, string salt);
    }
}