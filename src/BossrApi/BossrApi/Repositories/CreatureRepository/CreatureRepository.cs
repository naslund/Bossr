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
                await conn.ExecuteAsync("INSERT INTO Creatures (Name) VALUES (@Name)", creature);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Creatures WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ICreature>> ReadAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var creatures = await conn.QueryAsync<Creature>("SELECT * FROM Creatures");
                return creatures;
            }
        }

        public async Task<ICreature> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var creature = await conn.QuerySingleOrDefaultAsync<Creature>("SELECT * FROM Creatures WHERE Id = @Id", new { Id = id });
                return creature;
            }
        }

        public async Task<ICreature> ReadAsync(string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                var creature = await conn.QuerySingleOrDefaultAsync<Creature>("SELECT * FROM Creatures WHERE Name = @Name", new { Name = name });
                return creature;
            }
        }

        public async Task UpdateAsync(ICreature creature)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Creatures SET Name = @Name WHERE Id = @Id", creature);
            }
        }
    }
}