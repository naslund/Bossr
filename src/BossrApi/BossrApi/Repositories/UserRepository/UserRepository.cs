using System.Collections.Generic;
using System.Threading.Tasks;
using BossrApi.Interfaces;
using Dapper;
using BossrApi.Services.Security.SaltGeneratorService;
using BossrApi.Services.Security.HashGeneratorService;
using BossrApi.Models.Interfaces;
using BossrApi.Models.Pocos;
using Dapper.Contrib.Extensions;

namespace BossrApi.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        private readonly ISaltGeneratorService saltGeneratorService;
        private readonly IHashGeneratorService hashGeneratorService;

        public UserRepository(IDbConnectionFactory dbConnectionFactory,
            ISaltGeneratorService saltGeneratorService,
            IHashGeneratorService hashGeneratorService)
        {
            this.dbConnectionFactory = dbConnectionFactory;
            this.saltGeneratorService = saltGeneratorService;
            this.hashGeneratorService = hashGeneratorService;
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
            var salt = saltGeneratorService.GenerateSalt();
            var hashedPassword = hashGeneratorService.GenerateSaltedHash(password, salt);

            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("INSERT INTO Users (Username, HashedPassword, Salt) VALUES (@Username, @HashedPassword, @Salt)", new { Username = username, HashedPassword = hashedPassword, Salt = salt });
            }
        }

        public async Task UpdatePasswordAsync(string id, string password)
        {
            var salt = saltGeneratorService.GenerateSalt();
            var hashedPassword = hashGeneratorService.GenerateSaltedHash(password, salt);
            
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