using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Services
{
    public interface ICharacterManager
    {
        Task<IEnumerable<ICharacter>> GetAllRelatedCharactersAsync(int userId);
    }

    public class CharacterManager : ICharacterManager
    {
        private readonly ICharacterRepository repository;

        public CharacterManager(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ICharacter>> GetAllRelatedCharactersAsync(int userId)
        {
            return await repository.ReadAllByUserIdAsync(userId);
        }
    }
}
