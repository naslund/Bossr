﻿using BossrApi.Interfaces;
using BossrApi.Models.Entities;
using BossrApi.Models.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.UserRepository
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
                await conn.ExecuteAsync("INSERT INTO Users (Username, HashedPassword, Salt) VALUES (@Username, @HashedPassword, @Salt)", user);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id });
            }
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

        public async Task UpdateAsync(IUser user)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Users SET Username = @Username, HashedPassword = @HashedPassword, Salt = @Salt WHERE Id = @Id", user);
            }
        }
    }
}