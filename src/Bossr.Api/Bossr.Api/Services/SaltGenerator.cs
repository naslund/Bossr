using System;
using System.Security.Cryptography;

namespace Bossr.Api.Services
{
    public interface ISaltGenerator
    {
        string GenerateSalt();
    }

    public class SaltGenerator : ISaltGenerator
    {
        public string GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }
    }
}