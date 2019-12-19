using Bossr.Api.Repositories;
using Bossr.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    public class UserCharactersController : Controller
    {
        private readonly ICharacterManager manager;
        private readonly ICharacterRepository repository;
        private readonly IUserAccessValidator accessValidator;
        private readonly IUserIdentityReader userIdentityReader;

        public UserCharactersController(
            ICharacterManager manager,
            ICharacterRepository repository,
            IUserAccessValidator accessValidator,
            IUserIdentityReader userIdentityReader)
        {
            this.manager = manager;
            this.repository = repository;
            this.accessValidator = accessValidator;
            this.userIdentityReader = userIdentityReader;
        }

        [HttpGet("api/users/{userId}/characters")]
        [Authorize(Policy = "ReadUserCharacters")]
        public async Task<IActionResult> Get(int userId)
        {
            var isAllowedToAccess = accessValidator.IsCurrentUserAllowedToAccessUserResources(userId);
            if (!isAllowedToAccess)
                return Forbid();

            var characters = await repository.ReadAllByUserIdAsync(userId);
            return Ok(characters);
        }
    }
}
