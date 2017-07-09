using Bossr.Api.Attributes;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository repository;

        public CategoriesController(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteCategories")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadCategories")]
        public async Task<IActionResult> Get()
        {
            var categories = await repository.ReadAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadCategories")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await repository.ReadByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateCategories")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var category = await repository.ReadByIdAsync(id);
            if (category == null)
                return NotFound();

            patch.ApplyTo(category);
            await repository.UpdateAsync(category);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Name not available.")]
        [Authorize(Policy = "CreateCategories")]
        public async Task<IActionResult> Post([FromBody]Category request)
        {
            await repository.CreateAsync(request);
            var category = await repository.ReadByIdAsync(request.Id);
            return Created($"/api/categories/{category.Id}", category);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateCategories")]
        public async Task<IActionResult> Put(int id, [FromBody]Category request)
        {
            request.Id = id;

            await repository.UpdateAsync(request);
            return Ok();
        }
    }
}
