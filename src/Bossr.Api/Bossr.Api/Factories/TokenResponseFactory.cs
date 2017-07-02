using Bossr.Api.Middleware;
using Bossr.Api.Models.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace Bossr.Api.Factories
{
    public interface ITokenResponseFactory
    {
        TokenResponse CreateTokenResponse(JwtSecurityToken token, TokenProviderOptions options);
    }

    public class TokenResponseFactory : ITokenResponseFactory
    {
        private readonly JwtSecurityTokenHandler tokenHandler;

        public TokenResponseFactory(JwtSecurityTokenHandler tokenHandler)
        {
            this.tokenHandler = tokenHandler;
        }

        public TokenResponse CreateTokenResponse(JwtSecurityToken token, TokenProviderOptions options)
        {
            var encodedToken = tokenHandler.WriteToken(token);
            var expiresInSeconds = (int)options.Expiration.TotalSeconds;

            var response = new TokenResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = expiresInSeconds
            };

            return response;
        }
    }
}
