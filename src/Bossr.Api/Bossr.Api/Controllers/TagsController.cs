using Bossr.Api.Attributes;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("tags")]
    public class TagsController : Controller
    {
        private readonly ITagRepository repository;

        public TagsController(ITagRepository repository)
        {
            this.repository = repository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteTags")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadTags")]
        public async Task<IActionResult> Get()
        {
            var tags = await repository.ReadAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadTags")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await repository.ReadByIdAsync(id);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateTags")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var tag = await repository.ReadByIdAsync(id);
            if (tag == null)
                return NotFound();

            patch.ApplyTo(tag);
            await repository.UpdateAsync(tag);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Name not available.")]
        [Authorize(Policy = "CreateTags")]
        public async Task<IActionResult> Post([FromBody]Tag request)
        {
            await repository.CreateAsync(request);
            var tag = await repository.ReadByIdAsync(request.Id);
            return Created($"/api/tags/{tag.Id}", tag);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateTags")]
        public async Task<IActionResult> Put(int id, [FromBody]Tag request)
        {
            request.Id = id;

            await repository.UpdateAsync(request);
            return Ok();
        }
    }
}
