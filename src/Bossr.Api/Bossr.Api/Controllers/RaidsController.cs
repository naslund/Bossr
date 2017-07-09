using Bossr.Api.Mappers;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("raids")]
    public class RaidsController : Controller
    {
        private readonly IRaidRepository repository;
        private readonly IRaidMapper raidMapper;

        public RaidsController(IRaidRepository repository, IRaidMapper raidMapper)
        {
            this.repository = repository;
            this.raidMapper = raidMapper;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteRaids")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadRaids")]
        public async Task<IActionResult> Get()
        {
            var raids = await repository.ReadAllAsync();
            return Ok(raids);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadRaids")]
        public async Task<IActionResult> Get(int id)
        {
            var raid = await repository.ReadByIdAsync(id);
            if (raid == null)
                return NotFound();
            
            return Ok(raid);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateRaids")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var raid = await repository.ReadByIdAsync(id);
            if (raid == null)
                return NotFound();
            
            patch.ApplyTo(raid);
            
            await repository.UpdateAsync(raid);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "CreateRaids")]
        public async Task<IActionResult> Post([FromBody]Raid raid)
        {
            await repository.CreateAsync(raid);
            return Created($"/api/raids/{raid.Id}", raid);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateRaids")]
        public async Task<IActionResult> Put(int id, [FromBody]Raid raid)
        {
            raid.Id = id;
            await repository.UpdateAsync(raid);
            return Ok();
        }
    }
}
