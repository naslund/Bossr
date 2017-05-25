using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface ISpawnRepository : ICrudable<ISpawn> { }

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
                spawn.Id = await conn.QuerySingleAsync<int>("INSERT INTO Spawns (CreatureId, RaidId, Amount) VALUES (@CreatureId, @RaidId, @Amount) SELECT CAST(SCOPE_IDENTITY() as int)", spawn);
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
                await conn.ExecuteAsync("UPDATE Spawns SET CreatureId = @CreatureId, RaidId = @RaidId, Amount = @Amount WHERE Id = @Id", spawn);
            }
        }
    }
}
