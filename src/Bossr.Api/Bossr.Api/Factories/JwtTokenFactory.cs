using Bossr.Api.Middleware;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Bossr.Api.Factories
{
    public interface IJwtTokenFactory
    {
        JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, TokenProviderOptions options);
    }

    public class JwtTokenFactory : IJwtTokenFactory
    {
        public JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, TokenProviderOptions options)
        {
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);

            return token;
        }
    }
}
