using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface IPositionRepository : ICrudable<IPosition> { }

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
                position.Id = await conn.QuerySingleAsync<int>("INSERT INTO Positions (Name, X, Y, Z) VALUES (@Name, @X, @Y, @Z) SELECT CAST(SCOPE_IDENTITY() as int)", position);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Positions WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IPosition>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Position>("SELECT * FROM Positions");
            }
        }

        public async Task<IPosition> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Position>("SELECT * FROM Positions WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(IPosition position)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Positions SET Name = @Name, X = @X, Y = @Y, Z = @Z WHERE Id = @Id", position);
            }
        }
    }
}
