using AutoMapper;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/raids")]
    [Authorize(Roles = "admin")]
    public class RaidsController : Controller
    {
        private readonly IRaidRepository repository;

        public RaidsController(IRaidRepository repository)
        {
            this.repository = repository;
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
            var raidsDto = Mapper.Map<IEnumerable<RaidDto>>(raids);
            return Ok(raidsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var raid = await repository.ReadByIdAsync(id);
            if (raid == null)
                return NotFound();

            var raidDto = Mapper.Map<RaidDto>(raid);
            return Ok(raidDto);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var raid = await repository.ReadByIdAsync(id);
            if (raid == null)
                return NotFound();

            var raidDto = Mapper.Map<RaidDto>(raid);

            patch.ApplyTo(raidDto);

            var patchedRaid = Mapper.Map<Raid>(raidDto);

            await repository.UpdateAsync(patchedRaid);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RaidDto request)
        {
            var raid = Mapper.Map<Raid>(request);

            await repository.CreateAsync(raid);

            var response = await repository.ReadByIdAsync(raid.Id);
            var responseDto = Mapper.Map<RaidDto>(response);

            return Created($"/api/raids/{responseDto.Id}", responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]RaidDto request)
        {
            request.Id = id;

            var raid = Mapper.Map<Raid>(request);

            await repository.UpdateAsync(raid);
            return Ok();
        }
    }
}
