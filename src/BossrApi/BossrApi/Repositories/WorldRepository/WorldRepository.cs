using BossrApi.Interfaces;
using BossrApi.Models.Pocos;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.WorldRepository
{
    public class WorldRepository : IWorldRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;
        public WorldRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("INSERT INTO Worlds (Name) VALUES (@Name)", new { Name = name });
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Worlds WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<World>> ReadAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var worlds = await conn.QueryAsync<World>("SELECT * FROM Worlds");
                return worlds;
            }
        }

        public async Task<World> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var world = await conn.QuerySingleOrDefaultAsync<World>("SELECT * FROM Worlds WHERE Id = @Id", new { Id = id });
                return world;
            }
        }

        public async Task<World> ReadAsync(string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var world = await conn.QuerySingleOrDefaultAsync<World>("SELECT * FROM Worlds WHERE Name = @Name", new { Name = name });
                return world;
            }
        }

        public async Task UpdateNameAsync(int id, string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Worlds SET Name = @Name WHERE Id = @Id", new { Id = id, Name = name });
            }
        }
    }
}
