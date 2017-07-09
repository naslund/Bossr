using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("spawns")]
    public class SpawnsController : Controller
    {
        private readonly ISpawnRepository spawnRepository;

        public SpawnsController(ISpawnRepository spawnRepository)
        {
            this.spawnRepository = spawnRepository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteSpawns")]
        public async Task<IActionResult> Delete(int id)
        {
            await spawnRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadSpawns")]
        public async Task<IActionResult> Get()
        {
            var spawns = await spawnRepository.ReadAllAsync();
            return Ok(spawns);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadSpawns")]
        public async Task<IActionResult> Get(int id)
        {
            var spawn = await spawnRepository.ReadByIdAsync(id);
            if (spawn == null)
                return NotFound();

            return Ok(spawn);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateSpawns")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var spawn = await spawnRepository.ReadByIdAsync(id);
            if (spawn == null)
                return NotFound();

            patch.ApplyTo(spawn);
            await spawnRepository.UpdateAsync(spawn);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "CreateSpawns")]
        public async Task<IActionResult> Post([FromBody]Spawn request)
        {
            await spawnRepository.CreateAsync(request);
            var spawn = await spawnRepository.ReadByIdAsync(request.Id);
            return Created($"/api/spawns/{spawn.Id}", spawn);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateSpawns")]
        public async Task<IActionResult> Put(int id, [FromBody]Spawn request)
        {
            request.Id = id;

            await spawnRepository.UpdateAsync(request);
            return Ok();
        }
    }
}
