using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface ITagRepository : ICrudable<ITag> { }

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
                tag.Id = await conn.QuerySingleAsync<int>("INSERT INTO Tags (Name, CategoryId) VALUES (@Name, @CategoryId) SELECT CAST(SCOPE_IDENTITY() as int)", tag);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Tags WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ITag>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Tag>("SELECT * FROM Tags");
            }
        }

        public async Task<ITag> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Tag>("SELECT * FROM Tags WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(ITag tag)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Tags SET Name = @Name, CategoryId = @CategoryId WHERE Id = @Id", tag);
            }
        }
    }
}
