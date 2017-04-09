using BossrApi.Attributes;
using BossrApi.Models.Entities;
using BossrApi.Repositories.WorldRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/worlds")]
    [Authorize(Roles = "admin")]
    public class WorldsController : Controller
    {
        private readonly IWorldRepository worldRepository;

        public WorldsController(IWorldRepository worldRepository)
        {
            this.worldRepository = worldRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await worldRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var worlds = await worldRepository.ReadAsync();
            return Ok(worlds);
        }

        [HttpGet("{id}", Name = "GetWorld")]
        public async Task<IActionResult> Get(int id)
        {
            var world = await worldRepository.ReadAsync(id);
            if (world == null)
                return NotFound();

            return Ok(world);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var world = await worldRepository.ReadAsync(id);
            if (world == null)
                return NotFound();

            patch.ApplyTo(world);
            await worldRepository.UpdateAsync(world);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "World name not available.")]
        public async Task<IActionResult> Post([FromBody]World request)
        {
            await worldRepository.CreateAsync(request);
            var world = await worldRepository.ReadAsync(request.Name);
            return CreatedAtRoute("GetWorld", new { controller = "api/worlds", id = world.Id }, world);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]World request)
        {
            request.Id = id;
            await worldRepository.UpdateAsync(request);
            return Ok();
        }
    }
}