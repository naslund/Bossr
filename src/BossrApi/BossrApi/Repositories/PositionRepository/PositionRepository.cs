using BossrApi.Factories;
using BossrLib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public PositionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IPosition position)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                position.Id = await conn.QuerySingleAsync<int>("spInsertPosition", position, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spDeletePositionById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<IPosition>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Position>("spGetPositions", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IPosition> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Position>("spGetPositionById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateAsync(IPosition position)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spUpdatePosition", position, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
