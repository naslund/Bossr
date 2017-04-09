using BossrApi.Interfaces;
using BossrApi.Models.Entities;
using BossrApi.Models.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.SpawnRepository
{
    public class SpawnRepository : ISpawnRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public SpawnRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(ISpawn spawn)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("INSERT INTO Spawns (WorldId, CreatureId, TimeMin, TimeMax) VALUES (@WorldId, @CreatureId, @TimeMin, @TimeMax)", spawn);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Spawns WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ISpawn>> ReadAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Spawn>("SELECT * FROM Spawns");
            }
        }

        public async Task<ISpawn> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Spawn>("SELECT * FROM Spawns WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(ISpawn spawn)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Spawns SET CreatureId = @CreatureId, WorldId = @WorldId, TimeMin = @TimeMin, TimeMax = @TimeMax WHERE Id = @Id", spawn);
            }
        }
    }
}