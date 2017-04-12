using BossrApi.Interfaces;
using BossrApi.Models.Entities;
using BossrApi.Models.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BossrApi.Repositories.ScrapeRepository
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
                scrape.Id = await conn.QuerySingleAsync<int>("INSERT INTO Scrapes (TimeMinUtc) VALUES (@TimeMinUtc) SELECT CAST(SCOPE_IDENTITY() as int)", scrape);
            }
        }

        public async Task DeleteAsync(int id)
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

        public async Task<IScrape> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Scrape>("SELECT * FROM Scrapes WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(IScrape scrape)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Scrapes SET TimeMinUtc = @TimeMinUtc WHERE Id = @Id", scrape);
            }
        }
    }
}
