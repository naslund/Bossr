using Bossr.Api.Factories;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public TagRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(ITag tag)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                tag.Id = await conn.QuerySingleAsync<int>("spInsertTag", tag, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spDeleteTagById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ITag>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Tag>("spGetTags", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ITag> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Tag>("spGetTagById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateAsync(ITag tag)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spUpdateTag", tag, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
