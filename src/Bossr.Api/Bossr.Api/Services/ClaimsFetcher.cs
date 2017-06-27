using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bossr.Api.Services
{
    public interface IClaimsFetcher
    {
        Task<IEnumerable<Claim>> FetchClaimsAsync(IUser user);
    }

    public class ClaimsFetcher : IClaimsFetcher
    {
        private readonly IScopeRepository scopeRepository;

        public ClaimsFetcher(IScopeRepository scopeRepository)
        {
            this.scopeRepository = scopeRepository;
        }

        public async Task<IEnumerable<Claim>> FetchClaimsAsync(IUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("roles", "admin")
            };

            var scopes = await scopeRepository.ReadAllByUserIdAsync(user.Id);

            foreach (var scope in scopes)
            {
                var claim = new Claim("scope", scope.Name);
                claims.Add(claim);
            }

            return claims;
        }
    }
}
