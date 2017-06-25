using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
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
                raid.Id = await conn.QuerySingleAsync<int>("INSERT INTO Raids (FrequencyMin, FrequencyMax) VALUES (@FrequencyMin, @FrequencyMax) SELECT CAST(SCOPE_IDENTITY() as int)", raid);
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
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Raid>("SELECT * FROM Raids");
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
                await conn.ExecuteAsync("UPDATE Raids SET FrequencyMin = @FrequencyMin, FrequencyMax = @FrequencyMax WHERE Id = @Id", raid);
            }
        }
    }
}
