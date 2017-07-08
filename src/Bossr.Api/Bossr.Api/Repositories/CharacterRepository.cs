using Bossr.Api.Factories;
using Bossr.Api.Repositories.Interfaces;
using Bossr.Lib.Models.Entities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Repositories
{
    public interface ICharacterRepository : ICrudable<ICharacter> { }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CharacterRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateAsync(ICharacter character)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                character.Id = await conn.QuerySingleAsync<int>("INSERT INTO Characters (Name, WorldId, RaidId, UserId) VALUES (@Name, @WorldId, @RaidId, @UserId) SELECT CAST(SCOPE_IDENTITY() as int)", character);
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("DELETE FROM Characters WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<IEnumerable<ICharacter>> ReadAllAsync()
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QueryAsync<Character>("SELECT * FROM Characters");
            }
        }

        public async Task<ICharacter> ReadByIdAsync(int id)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<Character>("SELECT * FROM Characters WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task UpdateAsync(ICharacter character)
        {
            using (var conn = dbConnectionFactory.CreateConnection())
            {
                await conn.ExecuteAsync("UPDATE Characters SET Name = @Name, WorldId = @WorldId, RaidId = @RaidId, UserId = @UserId WHERE Id = @Id", character);
            }
        }
    }
}
