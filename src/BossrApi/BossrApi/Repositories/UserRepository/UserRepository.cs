using BossrApi.Interfaces;
using BossrApi.Models.Interfaces;
using BossrApi.Models.Pocos;
using BossrApi.Services.HashGenerator;
using BossrApi.Services.SaltGenerator;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly ISaltGenerator saltGenerator;
        private readonly IHashGenerator hashGenerator;

        public UserRepository(IDbConnectionFactory dbConnectionFactory,
            ISaltGenerator saltGenerator,
            IHashGenerator hashGenerator)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.saltGenerator = saltGenerator;
            this.hashGenerator = hashGenerator;
        }

        public async Task<IEnumerable<IUser>> ReadAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var users = await conn.QueryAsync<User>("SELECT * FROM Users");
                return users;
            }
        }

        public async Task<IUser> ReadAsync(string username)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var user = await conn.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username });
                return user;
            }
        }

        public async Task<IUser> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var user = await conn.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
                return user;
            }
        }

        public async Task CreateAsync(string username, string password)
        {
            var salt = saltGenerator.GenerateSalt();
            var hashedPassword = hashGenerator.GenerateSaltedHash(password, salt);

            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("INSERT INTO Users (Username, HashedPassword, Salt) VALUES (@Username, @HashedPassword, @Salt)", new { Username = username, HashedPassword = hashedPassword, Salt = salt });
            }
        }

        public async Task UpdatePasswordAsync(string id, string password)
        {
            var salt = saltGenerator.GenerateSalt();
            var hashedPassword = hashGenerator.GenerateSaltedHash(password, salt);
            
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Users SET HashedPassword = @HashedPassword, Salt = @Salt WHERE Id = @Id", new { HashedPassword = hashedPassword, Salt = salt, Id = id });
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
            }
        }
    }
}