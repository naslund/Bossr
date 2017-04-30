using BossrApi.Attributes;
using BossrApi.Repositories;
using BossrLib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/tags")]
    [Authorize(Roles = "admin")]
    public class TagsController : Controller
    {
        private readonly ITagRepository repository;

        public TagsController(ITagRepository repository)
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
            var tags = await repository.ReadAllAsync();
            return Ok(tags);
        }

        [HttpGet("{id}", Name = "GetTag")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await repository.ReadByIdAsync(id);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPatch("{id}")]
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
        public async Task<IActionResult> Post([FromBody]Tag request)
        {
            await repository.CreateAsync(request);
            var tag = await repository.ReadByIdAsync(request.Id);
            return CreatedAtRoute("GetTag", new { controller = "api/tags", id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Tag request)
        {
            request.Id = id;

            await repository.UpdateAsync(request);
            return Ok();
        }
    }
}
