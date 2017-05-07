using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface ICategoryRepository :
        ICrudable<ICategory>
    {
    }

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
                category.Id = await conn.QuerySingleAsync<int>("spInsertCategory", category, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spDeleteCategoryById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ICategory>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Category>("spGetCategories", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ICategory> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Category>("spGetCategoryById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateAsync(ICategory category)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spUpdateCategory", category, commandType: CommandType.StoredProcedure);
            }
        }
    }
}