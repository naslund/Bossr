using BossrApi.Models.Entities;
using BossrApi.Repositories.SpawnRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/spawns")]
    [Authorize(Roles = "admin")]
    public class SpawnsController : Controller
    {
        private readonly ISpawnRepository spawnRepository;

        public SpawnsController(ISpawnRepository spawnRepository)
        {
            this.spawnRepository = spawnRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var spawns = await spawnRepository.ReadAllAsync();
            return Ok(spawns);
        }

        [HttpGet("{id}", Name = "GetSpawn")]
        public async Task<IActionResult> Get(int id)
        {
            var spawn = await spawnRepository.ReadAsync(id);
            if (spawn == null)
                return NotFound();

            return Ok(spawn);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var spawn = await spawnRepository.ReadAsync(id);
            if (spawn == null)
                return NotFound();

            patch.ApplyTo(spawn);
            await spawnRepository.UpdateAsync(spawn);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Spawn request)
        {
            await spawnRepository.CreateAsync(request);
            var spawn = await spawnRepository.ReadAsync(request.Id);
            return CreatedAtRoute("GetSpawn", new { controller = "api/spawns", id = spawn.Id }, spawn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Spawn request)
        {
            request.Id = id;

            await spawnRepository.UpdateAsync(request);
            return Ok();
        }
    }
}