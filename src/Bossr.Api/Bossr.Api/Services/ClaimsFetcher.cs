using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString(), ClaimValueTypes.Integer));

            var scopes = await scopeRepository.ReadAllByUserIdAsync(user.Id);
            claims.AddRange(scopes.Select(x => new Claim("scope", x.Name)));

            return claims;
        }
    }
}
