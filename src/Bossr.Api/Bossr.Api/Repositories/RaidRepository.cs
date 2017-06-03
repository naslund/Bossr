using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface IRaidRepository : ICrudable<IRaid> { }

    public class RaidRepository : IRaidRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public RaidRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IRaid raid)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                raid.Id = await conn.QuerySingleAsync<int>("INSERT INTO Raids (Name, FrequencyMin, FrequencyMax) VALUES (@Name, @FrequencyMin, @FrequencyMax) SELECT CAST(SCOPE_IDENTITY() as int)", raid);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Raids WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IRaid>> ReadAllAsync()
        {
            var sql = @"SELECT * FROM Raids;
                        SELECT * FROM Spawns;
                        SELECT * FROM Creatures;
                        SELECT * FROM Positions";

            using (var conn = dbConnectionFactory.CreateConnection())
            {
                using (var multi = await conn.QueryMultipleAsync(sql))
                {
                    var raids = multi.Read<Raid>();
                    var spawns = multi.Read<Spawn>();
                    var creatures = multi.Read<Creature>();
                    var positions = multi.Read<Position>();

                    foreach (var spawn in spawns)
                    {
                        spawn.Creature = creatures.Single(x => x.Id == spawn.CreatureId);
                        spawn.Positions = positions.Where(x => x.SpawnId == spawn.Id);
                    }

                    foreach (var raid in raids)
                    {
                        raid.Spawns = spawns.Where(x => x.RaidId == raid.Id);
                    }

                    return raids;
                }
            }
        }

        public async Task<IRaid> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Raid>("SELECT * FROM Raids WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(IRaid raid)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Raids SET Name = @Name, FrequencyMin = @FrequencyMin, FrequencyMax = @FrequencyMax WHERE Id = @Id", raid);
            }
        }
    }
}
