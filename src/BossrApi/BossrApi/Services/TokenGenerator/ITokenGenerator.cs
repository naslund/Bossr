using BossrApi.Middleware;
using BossrApi.Models.Entities;
using BossrApi.Models.Responses;

namespace BossrApi.Services
{
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(TokenProviderOptions options, IUser user);
    }
}