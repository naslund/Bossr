using Bossr.Api.Factories;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public class ScrapeRepository : IScrapeRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public ScrapeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IScrape scrape)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                scrape.Id = await conn.QuerySingleAsync<int>("spInsertScrape", scrape, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spDeleteScrapeById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<IScrape>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Scrape>("spGetScrapes", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IScrape> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Scrape>("spGetScrapeById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IScrape> ReadLatest()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Scrape>("spGetScrapeByLatestDate", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateAsync(IScrape scrape)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spUpdateScrape", scrape, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
