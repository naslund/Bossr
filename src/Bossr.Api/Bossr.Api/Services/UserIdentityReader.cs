using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Bossr.Api.Services
{
    public interface IUserIdentityReader
    {
        int GetUserId(ClaimsPrincipal user);
    }

    public class UserIdentityReader : IUserIdentityReader
    {
        public int GetUserId(ClaimsPrincipal user)
        {
            var claim = user.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sub);
            return int.Parse(claim.Value);
        }
    }
}
