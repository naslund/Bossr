using Bossr.Api.Mappers;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("scrapes")]
    public class ScrapesController : Controller
    {
        private readonly IScrapeRepository scrapeRepository;
        private readonly IScrapeMapper scrapeMapper;

        public ScrapesController(IScrapeRepository scrapeRepository, IScrapeMapper scrapeMapper)
        {
            this.scrapeRepository = scrapeRepository;
            this.scrapeMapper = scrapeMapper;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteScrapes")]
        public async Task<IActionResult> Delete(int id)
        {
            await scrapeRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadScrapes")]
        public async Task<IActionResult> Get()
        {
            var scrapes = await scrapeRepository.ReadAllAsync();
            return Ok(scrapes);
        }

        [HttpGet("latest")]
        [Authorize(Policy = "ReadScrapes")]
        public async Task<IActionResult> GetLatest()
        {
            var scrape = await scrapeRepository.ReadLatest();
            return Ok(scrape);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadScrapes")]
        public async Task<IActionResult> Get(int id)
        {
            var scrape = await scrapeRepository.ReadByIdAsync(id);
            if (scrape == null)
                return NotFound();
            
            return Ok(scrape);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateScrapes")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var scrape = await scrapeRepository.ReadByIdAsync(id);
            if (scrape == null)
                return NotFound();

            patch.ApplyTo(scrape);
            await scrapeRepository.UpdateAsync(scrape);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "CreateScrapes")]
        public async Task<IActionResult> Post([FromBody]Scrape scrape)
        {
            await scrapeRepository.CreateAsync(scrape);
            return Created($"/api/scrapes/{scrape.Id}", scrape);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateScrapes")]
        public async Task<IActionResult> Put(int id, [FromBody]Scrape scrape)
        {
            scrape.Id = id;
            await scrapeRepository.UpdateAsync(scrape);
            return Ok();
        }
    }
}
