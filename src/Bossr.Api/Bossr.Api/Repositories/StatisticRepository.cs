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
    public interface IStatisticRepository : ICrudable<IStatistic>
    {
        Task<IEnumerable<IStatistic>> ReadAllByWorldIdAsync(int worldId);
    }

    public class StatisticRepository : IStatisticRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public StatisticRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IStatistic statistic)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                statistic.Id = await conn.QuerySingleAsync<int>("INSERT INTO [Statistics] (WorldId, CreatureId, ScrapeId, Amount) VALUES (@WorldId, @CreatureId, @ScrapeId, @Amount) SELECT CAST(SCOPE_IDENTITY() as int)", statistic);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM [Statistics] WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IStatistic>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Statistic>("SELECT * FROM [Statistics]");
            }
        }

        public async Task<IEnumerable<IStatistic>> ReadAllByWorldIdAsync(int worldId)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Statistic>("SELECT * FROM [Statistics] WHERE WorldId = @WorldId", new { WorldId = worldId });
            }
        }

        public async Task<IStatistic> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Statistic>("SELECT * FROM [Statistics] WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(IStatistic statistic)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE [Statistics] SET WorldId = @WorldId, CreatureId = @CreatureId, ScrapeId = @ScrapeId, Amount = @Amount WHERE Id = @Id", statistic);
            }
        }
    }
}
