using BossrApi.Middleware.TokenProvider;
using BossrApi.Models.Interfaces;
using BossrApi.Models.Responses;

namespace BossrApi.Services
{
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(TokenProviderOptions options, IUser user);
    }
}
