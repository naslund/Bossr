using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface IWorldRepository : ICrudable<IWorld>, IListableByIsMonitored<IWorld>, IReadableByName<IWorld> { }

    public class WorldRepository : IWorldRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public WorldRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IWorld world)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                world.Id = await conn.QuerySingleAsync<int>("INSERT INTO Worlds (Name, IsMonitored) VALUES (@Name, @IsMonitored) SELECT CAST(SCOPE_IDENTITY() as int)", world);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Worlds WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IWorld>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<World>("SELECT * FROM Worlds");
            }
        }

        public async Task<IEnumerable<IWorld>> ReadAllByIsMonitoredAsync(bool isMonitored)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<World>("SELECT * FROM Worlds WHERE IsMonitored = @IsMonitored", new { IsMonitored = isMonitored });
            }
        }

        public async Task<IWorld> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<World>("SELECT * FROM Worlds WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IWorld> ReadByNameAsync(string name)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<World>("SELECT * FROM Worlds WHERE Name = @Name", new { Name = name });
            }
        }

        public async Task UpdateAsync(IWorld world)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Worlds SET Name = @Name, IsMonitored = @IsMonitored WHERE Id = @Id", world);
            }
        }
    }
}