using BossrApi.Attributes;
using BossrApi.Models.Entities;
using BossrApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/creatures")]
    [Authorize(Roles = "admin")]
    public class CreaturesController : Controller
    {
        private readonly ICreatureRepository creatureRepository;

        public CreaturesController(ICreatureRepository creatureRepository)
        {
            this.creatureRepository = creatureRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await creatureRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var creatures = await creatureRepository.ReadAllAsync();
            return Ok(creatures);
        }

        [HttpGet("{id}", Name = "GetCreature")]
        public async Task<IActionResult> Get(int id)
        {
            var creature = await creatureRepository.ReadAsync(id);
            if (creature == null)
                return NotFound();

            return Ok(creature);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var creature = await creatureRepository.ReadAsync(id);
            if (creature == null)
                return NotFound();

            patch.ApplyTo(creature);
            await creatureRepository.UpdateAsync(creature);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Creature name not available.")]
        public async Task<IActionResult> Post([FromBody]Creature request)
        {
            await creatureRepository.CreateAsync(request);
            var creature = await creatureRepository.ReadAsync(request.Id);
            return CreatedAtRoute("GetCreature", new { controller = "api/creatures", id = creature.Id }, creature);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Creature request)
        {
            request.Id = id;

            await creatureRepository.UpdateAsync(request);
            return Ok();
        }
    }
}