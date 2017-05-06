using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Bossr.Api.Services
{
    public class HashGenerator : IHashGenerator
    {
        public string GenerateSaltedHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var saltBytes = Convert.FromBase64String(salt);
                var concatenatedBytes = passwordBytes.Concat(saltBytes).ToArray();

                var result = sha256.ComputeHash(concatenatedBytes);
                var saltedHash = Convert.ToBase64String(result);

                return saltedHash;
            }
        }
    }
}