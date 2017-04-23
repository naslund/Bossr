using BossrApi.Attributes;
using BossrApi.Models.Entities;
using BossrApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/cities")]
    [Authorize(Roles = "admin")]
    public class CitiesController : Controller
    {
        private readonly ICityRepository repository;

        public CitiesController(ICityRepository repository)
        {
            this.repository = repository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await repository.ReadAllAsync();
            return Ok(cities);
        }

        [HttpGet("{id}", Name = "GetCity")]
        public async Task<IActionResult> Get(int id)
        {
            var city = await repository.ReadAsync(id);
            if (city == null)
                return NotFound();
            
            return Ok(city);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var city = await repository.ReadAsync(id);
            if (city == null)
                return NotFound();

            patch.ApplyTo(city);
            await repository.UpdateAsync(city);
            return Ok();
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "City name not available.")]
        public async Task<IActionResult> Post([FromBody]City request)
        {
            await repository.CreateAsync(request);
            var city = await repository.ReadAsync(request.Id);
            return CreatedAtRoute("GetCity", new { controller = "api/cities", id = city.Id }, city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]City request)
        {
            request.Id = id;

            await repository.UpdateAsync(request);
            return Ok();
        }
    }
}
