using Bossr.Api.Attributes;
using Bossr.Api.Repositories;
using Bossr.Api.Services;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/characters")]
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository repository;
        private readonly ICharacterManager manager;
        private readonly IUserIdentityReader userIdentityReader;

        public CharactersController(
            ICharacterRepository repository,
            ICharacterManager manager,
            IUserIdentityReader userIdentityReader)
        {
            this.repository = repository;
            this.manager = manager;
            this.userIdentityReader = userIdentityReader;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteCharacters")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadCharacters")]
        public async Task<IActionResult> Get()
        {
            var characters = await repository.ReadAllAsync();
            return Ok(characters);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadCharacters")]
        public async Task<IActionResult> Get(int id)
        {
            var character = await repository.ReadByIdAsync(id);
            if (character == null)
                return NotFound();

            return Ok(character);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateCharacters")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var character = await repository.ReadByIdAsync(id);
            if (character == null)
                return NotFound();

            patch.ApplyTo(character);
            await repository.UpdateAsync(character);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Name not available.")]
        [Authorize(Policy = "CreateCharacters")]
        public async Task<IActionResult> Post([FromBody]Character character)
        {
            await repository.CreateAsync(character);
            return Created($"/api/characters/{character.Id}", character);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateCharacters")]
        public async Task<IActionResult> Put(int id, [FromBody]Character character)
        {
            character.Id = id;

            await repository.UpdateAsync(character);
            return Ok();
        }
    }
}
