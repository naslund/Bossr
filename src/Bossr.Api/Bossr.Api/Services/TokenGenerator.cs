using Bossr.Api.Factories;
using Bossr.Api.Middleware;
using Bossr.Api.Models.Responses;
using Bossr.Lib.Models.Entities;

namespace Bossr.Api.Services
{
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(TokenProviderOptions options, IUser user);
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly IClaimsFetcher claimsFetcher;
        private readonly IJwtTokenFactory jwtTokenFactory;
        private readonly ITokenResponseFactory tokenResponseFactory;

        public TokenGenerator(
            IClaimsFetcher claimsFetcher,
            ITokenResponseFactory tokenResponseFactory,
            IJwtTokenFactory jwtTokenFactory)
        {
            this.claimsFetcher = claimsFetcher;
            this.jwtTokenFactory = jwtTokenFactory;
            this.tokenResponseFactory = tokenResponseFactory;
        }

        public TokenResponse GenerateToken(TokenProviderOptions options, IUser user)
        {
            var claims = claimsFetcher.FetchClaims(user);
            var token = jwtTokenFactory.CreateJwtToken(claims, options);
            var response = tokenResponseFactory.CreateTokenResponse(token, options);
            return response;
        }
    }
}