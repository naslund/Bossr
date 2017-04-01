using BossrApi.Models.Interfaces;
using BossrApi.Services.Security.HashGeneratorService;

namespace BossrApi.Services.Security.PasswordValidatorService
{
    public class PasswordValidatorService : IPasswordValidatorService
    {
        private readonly IHashGeneratorService hashGeneratorService;

        public PasswordValidatorService(IHashGeneratorService hashGeneratorService)
        {
            this.hashGeneratorService = hashGeneratorService;
        }

        public bool IsPasswordValid(IUser user, string password)
        {
            if (user == null)
                return false;

            var saltedHash = hashGeneratorService.GenerateSaltedHash(password, user.Salt);
            return saltedHash == user.HashedPassword;
        }
    }
}
