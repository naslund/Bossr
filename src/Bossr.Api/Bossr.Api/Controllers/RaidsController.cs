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
    [Route("api/raids")]
    [Authorize(Roles = "admin")]
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
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var raids = await repository.ReadAllAsync();
            var raidsDto = raids.Select(x => raidMapper.MapToRaidDto(x));
            return Ok(raidsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var raid = await repository.ReadByIdAsync(id);
            if (raid == null)
                return NotFound();

            var raidDto = raidMapper.MapToRaidDto(raid);
            return Ok(raidDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var raid = await repository.ReadByIdAsync(id);
            if (raid == null)
                return NotFound();

            var raidDto = raidMapper.MapToRaidDto(raid);

            patch.ApplyTo(raidDto);

            var patchedRaid = raidMapper.MapToRaid(raidDto);

            await repository.UpdateAsync(patchedRaid);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RaidDto request)
        {
            var raid = raidMapper.MapToRaid(request);

            await repository.CreateAsync(raid);

            var response = await repository.ReadByIdAsync(raid.Id);
            var responseDto = raidMapper.MapToRaidDto(response);

            return Created($"/api/raids/{responseDto.Id}", responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]RaidDto request)
        {
            request.Id = id;

            var raid = raidMapper.MapToRaid(request);

            await repository.UpdateAsync(raid);
            return Ok();
        }
    }
}
