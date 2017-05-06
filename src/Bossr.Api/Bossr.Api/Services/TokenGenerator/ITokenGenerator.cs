using Bossr.Api.Middleware;
using Bossr.Api.Models.Responses;
using Bossr.Lib.Models.Entities;

namespace Bossr.Api.Services
{
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(TokenProviderOptions options, IUser user);
    }
}