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
            var sql = "SELECT * FROM Raids " +
                "JOIN Spawns ON Spawns.RaidId = Raids.Id " +
                "JOIN Creatures ON Spawns.CreatureId = Creatures.Id " +
                "JOIN Positions ON Positions.SpawnId = Spawns.Id";

            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var raids = new Dictionary<int, Raid>();
                var spawns = new Dictionary<int, Spawn>();
                var creatures = new Dictionary<int, Creature>();
                var positions = new Dictionary<int, Position>();

                await conn.QueryAsync<Raid, Spawn, Creature, Position, Raid>(sql, (raid, spawn, creature, position) => {
                    if (!raids.ContainsKey(raid.Id))
                        raids.Add(raid.Id, raid);

                    if (!spawns.ContainsKey(spawn.Id))
                        spawns.Add(spawn.Id, spawn);

                    if (!creatures.ContainsKey(creature.Id))
                        creatures.Add(creature.Id, creature);

                    if (!positions.ContainsKey(position.Id))
                        positions.Add(position.Id, position);

                    return null;
                });

                foreach(var spawn in spawns.Values)
                {
                    spawn.Creature = creatures.Values.Single(x => x.Id == spawn.CreatureId);
                    spawn.Positions = positions.Values.Where(x => x.SpawnId == spawn.Id);
                }

                foreach(var raid in raids.Values)
                {
                    raid.Spawns = spawns.Values.Where(x => x.RaidId == raid.Id);
                }

                return raids.Values;
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
