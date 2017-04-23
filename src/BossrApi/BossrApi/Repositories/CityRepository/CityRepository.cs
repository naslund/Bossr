using BossrApi.Factories;
using BossrApi.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BossrApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CityRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(ICity city)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                city.Id = await conn.QuerySingleAsync<int>("spInsertCity", city, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spDeleteCityById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ICity>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<City>("spGetCities", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ICity> ReadAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<City>("spGetCityById", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateAsync(ICity city)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("spUpdateCity", city, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
