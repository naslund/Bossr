using Bossr.Api.Attributes;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("creatures")]
    public class CreaturesController : Controller
    {
        private readonly ICreatureRepository creatureRepository;

        public CreaturesController(ICreatureRepository creatureRepository)
        {
            this.creatureRepository = creatureRepository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteCreatures")]
        public async Task<IActionResult> Delete(int id)
        {
            await creatureRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadCreatures")]
        public async Task<IActionResult> Get()
        {
            var creatures = await creatureRepository.ReadAllAsync();
            return Ok(creatures);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadCreatures")]
        public async Task<IActionResult> Get(int id)
        {
            var creature = await creatureRepository.ReadByIdAsync(id);
            if (creature == null)
                return NotFound();

            return Ok(creature);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateCreatures")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var creature = await creatureRepository.ReadByIdAsync(id);
            if (creature == null)
                return NotFound();

            patch.ApplyTo(creature);
            await creatureRepository.UpdateAsync(creature);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Creature name not available.")]
        [Authorize(Policy = "CreateCreatures")]
        public async Task<IActionResult> Post([FromBody]Creature request)
        {
            await creatureRepository.CreateAsync(request);
            var creature = await creatureRepository.ReadByIdAsync(request.Id);
            return Created($"/api/creatures/{creature.Id}", creature);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateCreatures")]
        public async Task<IActionResult> Put(int id, [FromBody]Creature request)
        {
            request.Id = id;

            await creatureRepository.UpdateAsync(request);
            return Ok();
        }
    }
}