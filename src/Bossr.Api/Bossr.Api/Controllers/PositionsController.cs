using Bossr.Api.Attributes;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/positions")]
    public class PositionsController : Controller
    {
        private readonly IPositionRepository repository;

        public PositionsController(IPositionRepository repository)
        {
            this.repository = repository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeletePositions")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadPositions")]
        public async Task<IActionResult> Get()
        {
            var position = await repository.ReadAllAsync();
            return Ok(position);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadPositions")]
        public async Task<IActionResult> Get(int id)
        {
            var position = await repository.ReadByIdAsync(id);
            if (position == null)
                return NotFound();

            return Ok(position);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdatePositions")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var position = await repository.ReadByIdAsync(id);
            if (position == null)
                return NotFound();

            patch.ApplyTo(position);
            await repository.UpdateAsync(position);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Name not available.")]
        [Authorize(Policy = "CreatePositions")]
        
        public async Task<IActionResult> Post([FromBody]Position request)
        {
            await repository.CreateAsync(request);
            var position = await repository.ReadByIdAsync(request.Id);
            return Created($"/api/positions/{position.Id}", position);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdatePositions")]
        public async Task<IActionResult> Put(int id, [FromBody]Position request)
        {
            request.Id = id;

            await repository.UpdateAsync(request);
            return Ok();
        }
    }
}
