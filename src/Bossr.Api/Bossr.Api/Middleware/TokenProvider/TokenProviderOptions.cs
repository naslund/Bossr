using Microsoft.IdentityModel.Tokens;
using System;

namespace Bossr.Api.Middleware
{
    public class TokenProviderOptions
    {
        public string Path { get; set; }
        public TimeSpan Expiration { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}