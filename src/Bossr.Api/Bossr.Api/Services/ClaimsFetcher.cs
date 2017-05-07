using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bossr.Api.Services
{
    public interface IClaimsFetcher
    {
        IEnumerable<Claim> FetchClaims(IUser user);
    }

    public class ClaimsFetcher : IClaimsFetcher
    {
        public IEnumerable<Claim> FetchClaims(IUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("roles", "admin")
            };

            return claims;
        }
    }
}
