using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using System.Threading.Tasks;

namespace Bossr.Api.Services
{
    public interface IUserManager
    {
        Task CreateUserAsync(string username, string password);

        Task UpdatePasswordAsync(int id, string password);
    }

    public class UserManager : IUserManager
    {
        private readonly IHashGenerator hashGenerator;
        private readonly ISaltGenerator saltGenerator;
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository,
            ISaltGenerator saltGenerator,
            IHashGenerator hashGenerator)
        {
            this.userRepository = userRepository;
            this.saltGenerator = saltGenerator;
            this.hashGenerator = hashGenerator;
        }

        public async Task CreateUserAsync(string username, string password)
        {
            var salt = saltGenerator.GenerateSalt();
            var hashedPassword = hashGenerator.GenerateSaltedHash(password, salt);

            var user = new User
            {
                Username = username,
                HashedPassword = hashedPassword,
                Salt = salt
            };

            await userRepository.CreateAsync(user);
        }

        public async Task UpdatePasswordAsync(int id, string password)
        {
            var salt = saltGenerator.GenerateSalt();
            var hashedPassword = hashGenerator.GenerateSaltedHash(password, salt);

            var user = await userRepository.ReadByIdAsync(id);
            user.Salt = salt;
            user.HashedPassword = hashedPassword;

            await userRepository.UpdateAsync(user);
        }
    }
}