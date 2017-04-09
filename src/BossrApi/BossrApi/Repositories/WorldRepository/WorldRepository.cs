using BossrApi.Interfaces;
using BossrApi.Models.Entities;
using BossrApi.Models.Interfaces;
using Dapper;
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

        public async Task CreateAsync(IWorld world)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("INSERT INTO Worlds (Name) VALUES (@Name)", world);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Worlds WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IWorld>> ReadAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var worlds = await conn.QueryAsync<World>("SELECT * FROM Worlds");
                return worlds;
            }
        }

        public async Task<IWorld> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var world = await conn.QuerySingleOrDefaultAsync<World>("SELECT * FROM Worlds WHERE Id = @Id", new { Id = id });
                return world;
            }
        }

        public async Task<IWorld> ReadAsync(string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var world = await conn.QuerySingleOrDefaultAsync<World>("SELECT * FROM Worlds WHERE Name = @Name", new { Name = name });
                return world;
            }
        }

        public async Task UpdateAsync(IWorld world)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Worlds SET Name = @Name WHERE Id = @Id", world);
            }
        }
    }
}