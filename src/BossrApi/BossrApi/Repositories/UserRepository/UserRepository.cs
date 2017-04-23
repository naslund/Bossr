using BossrApi.Factories;
using BossrApi.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IUser user)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                user.Id = await conn.QuerySingleAsync<int>("INSERT INTO Users (Username, HashedPassword, Salt) VALUES (@Username, @HashedPassword, @Salt) SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IUser>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<User>("SELECT * FROM Users");
            }
        }

        public async Task<IUser> ReadByUsername(string username)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username });
            }
        }

        public async Task<IUser> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(IUser user)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Users SET Username = @Username, HashedPassword = @HashedPassword, Salt = @Salt WHERE Id = @Id", user);
            }
        }
    }
}