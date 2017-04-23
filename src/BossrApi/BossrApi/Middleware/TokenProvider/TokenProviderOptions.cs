using Microsoft.IdentityModel.Tokens;
using System;

namespace BossrApi.Middleware
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30);
        public SigningCredentials SigningCredentials { get; set; }
    }
}