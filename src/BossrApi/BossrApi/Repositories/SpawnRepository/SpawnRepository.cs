using BossrApi.Factories;
using BossrApi.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BossrApi.Repositories
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
                spawn.Id = await conn.QuerySingleAsync<int>("INSERT INTO Spawns (WorldId, CreatureId, ScrapeId, Amount) VALUES (@WorldId, @CreatureId, @ScrapeId, @Amount) SELECT CAST(SCOPE_IDENTITY() as int)", spawn);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Spawns WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ISpawn>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Spawn>("SELECT * FROM Spawns");
            }
        }

        public async Task<IEnumerable<ISpawn>> ReadAllByWorldIdAsync(int worldId)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Spawn>("spGetSpawnsByWorldId", new { WorldId = worldId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ISpawn> ReadByIdAsync(int id)
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
                await conn.ExecuteAsync("UPDATE Spawns SET CreatureId = @CreatureId, WorldId = @WorldId, ScrapeId = @ScrapeId, Amount = @Amount WHERE Id = @Id", spawn);
            }
        }
    }
}