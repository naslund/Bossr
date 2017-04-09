using BossrApi.Interfaces;
using BossrApi.Models.Entities;
using BossrApi.Models.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.CreatureRepository
{
    public class CreatureRepository : ICreatureRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CreatureRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(ICreature creature)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("INSERT INTO Creatures (Name, SpawnRateHoursMin, SpawnRateHoursMax, IsMonitored) VALUES (@Name, @SpawnRateHoursMin, @SpawnRateHoursMax, @IsMonitored)", creature);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Creatures WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ICreature>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Creature>("SELECT * FROM Creatures");
            }
        }

        public async Task<IEnumerable<ICreature>> ReadAllAsync(bool isMonitored)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Creature>("SELECT * FROM Creatures WHERE IsMonitored = @IsMonitored", new { IsMonitored = isMonitored });
            }
        }

        public async Task<ICreature> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Creature>("SELECT * FROM Creatures WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<ICreature> ReadAsync(string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Creature>("SELECT * FROM Creatures WHERE Name = @Name", new { Name = name });
            }
        }

        public async Task UpdateAsync(ICreature creature)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Creatures SET Name = @Name, SpawnRateHoursMin = @SpawnRateHoursMin, SpawnRateHoursMax = @SpawnRateHoursMax, IsMonitored = @IsMonitored WHERE Id = @Id", creature);
            }
        }
    }
}