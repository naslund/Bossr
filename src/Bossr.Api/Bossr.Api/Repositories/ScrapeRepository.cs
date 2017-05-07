using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface IScrapeRepository : ICrudable<IScrape>
    {
        Task<IScrape> ReadLatest();
    }

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
                scrape.Id = await conn.QuerySingleAsync<int>("INSERT INTO Scrapes (Date) VALUES (@Date) SELECT CAST(SCOPE_IDENTITY() as int)", scrape);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Scrapes WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IScrape>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Scrape>("SELECT * FROM Scrapes");
            }
        }

        public async Task<IScrape> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Scrape>("SELECT * FROM Scrapes WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IScrape> ReadLatest()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Scrape>("SELECT TOP 1 * FROM Scrapes ORDER BY Date DESC");
            }
        }

        public async Task UpdateAsync(IScrape scrape)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Scrapes SET Date = @Date WHERE Id = @Id", scrape);
            }
        }
    }
}
