using BossrApi.Middleware.TokenProvider;
using BossrApi.Models.Interfaces;
using BossrApi.Models.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BossrApi.Services.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {
        public TokenResponse GenerateToken(TokenProviderOptions options, IUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("roles", "admin")
            };

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponse
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)options.Expiration.TotalSeconds
            };

            return response;
        }
    }
}
