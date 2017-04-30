using BossrApi.Middleware;
using BossrApi.Models.Responses;
using BossrLib.Models.Entities;

namespace BossrApi.Services
{
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(TokenProviderOptions options, IUser user);
    }
}