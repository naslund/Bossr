using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface ICategoryRepository : ICrudable<ICategory> { }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CategoryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(ICategory category)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                category.Id = await conn.QuerySingleAsync<int>("INSERT INTO Categories (Name) VALUES (@Name) SELECT CAST(SCOPE_IDENTITY() as int)", category);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Categories WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ICategory>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Category>("SELECT * FROM Categories");
            }
        }

        public async Task<ICategory> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Category>("SELECT * FROM Categories WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(ICategory category)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Categories SET Name = @Name WHERE Id = @Id", category);
            }
        }
    }
}