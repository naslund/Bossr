using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BossrApi.Middleware.TokenProvider
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30);
        public SigningCredentials SigningCredentials { get; set; }
    }
}
