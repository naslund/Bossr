using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/scrapes")]
    public class ScrapesController : Controller
    {
        private readonly IScrapeRepository repository;

        public ScrapesController(IScrapeRepository repository)
        {
            this.repository = repository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteScrapes")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadScrapes")]
        public async Task<IActionResult> Get()
        {
            var scrapes = await repository.ReadAllAsync();
            return Ok(scrapes);
        }

        [HttpGet("latest")]
        [Authorize(Policy = "ReadScrapes")]
        public async Task<IActionResult> GetLatest()
        {
            var scrape = await repository.ReadLatest();
            return Ok(scrape);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadScrapes")]
        public async Task<IActionResult> Get(int id)
        {
            var scrape = await repository.ReadByIdAsync(id);
            if (scrape == null)
                return NotFound();
            
            return Ok(scrape);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateScrapes")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var scrape = await repository.ReadByIdAsync(id);
            if (scrape == null)
                return NotFound();

            patch.ApplyTo(scrape);
            await repository.UpdateAsync(scrape);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "CreateScrapes")]
        public async Task<IActionResult> Post([FromBody]Scrape scrape)
        {
            await repository.CreateAsync(scrape);
            return Created($"/api/scrapes/{scrape.Id}", scrape);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateScrapes")]
        public async Task<IActionResult> Put(int id, [FromBody]Scrape scrape)
        {
            scrape.Id = id;
            await repository.UpdateAsync(scrape);
            return Ok();
        }
    }
}
