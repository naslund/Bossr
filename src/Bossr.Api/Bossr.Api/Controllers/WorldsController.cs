using Bossr.Api.Attributes;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("worlds")]
    public class WorldsController : Controller
    {
        private readonly IWorldRepository worldRepository;

        public WorldsController(IWorldRepository worldRepository)
        {
            this.worldRepository = worldRepository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteWorlds")]
        public async Task<IActionResult> Delete(int id)
        {
            await worldRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadWorlds")]
        public async Task<IActionResult> Get()
        {
            var worlds = await worldRepository.ReadAllAsync();
            return Ok(worlds);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadWorlds")]
        public async Task<IActionResult> Get(int id)
        {
            var world = await worldRepository.ReadByIdAsync(id);
            if (world == null)
                return NotFound();

            return Ok(world);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateWorlds")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var world = await worldRepository.ReadByIdAsync(id);
            if (world == null)
                return NotFound();

            patch.ApplyTo(world);
            await worldRepository.UpdateAsync(world);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "World name not available.")]
        [Authorize(Policy = "CreateWorlds")]
        public async Task<IActionResult> Post([FromBody]World request)
        {
            await worldRepository.CreateAsync(request);
            var world = await worldRepository.ReadByIdAsync(request.Id);
            return Created($"/api/worlds/{world.Id}", world);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateWorlds")]
        public async Task<IActionResult> Put(int id, [FromBody]World request)
        {
            request.Id = id;

            await worldRepository.UpdateAsync(request);
            return Ok();
        }
    }
}