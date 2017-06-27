using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface IScopeRepository : ICrudable<IScope>
    {
        Task<IEnumerable<IScope>> ReadAllByUserIdAsync(int userId);
    }

    public class ScopeRepository : IScopeRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public ScopeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(IScope scope)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                scope.Id = await conn.QuerySingleAsync<int>("INSERT INTO Scopes (Name) VALUES (@Name) SELECT CAST(SCOPE_IDENTITY() as int)", scope);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Scopes WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<IScope>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Scope>("SELECT * FROM Scopes");
            }
        }

        public async Task<IEnumerable<IScope>> ReadAllByUserIdAsync(int userId)
        {
            var sql = @"SELECT Scopes.* FROM Scopes
                        JOIN UserScopes ON Scopes.Id = UserScopes.ScopeId
                        JOIN Users ON Users.Id = UserScopes.UserId
                        WHERE Users.Id = @UserId";

            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Scope>(sql, new { UserId = userId });
            }
        }

        public async Task<IScope> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Scope>("SELECT * FROM Scopes WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(IScope scope)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Scopes SET Name = @Name WHERE Id = @Id", scope);
            }
        }
    }
}
