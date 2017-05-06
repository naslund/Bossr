using Microsoft.IdentityModel.Tokens;
using System;

namespace Bossr.Api.Middleware
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30);
        public SigningCredentials SigningCredentials { get; set; }
    }
}