using System;
using System.Security.Cryptography;

namespace BossrApi.Services.Security.SaltGeneratorService
{
    public class SaltGeneratorService : ISaltGeneratorService
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
