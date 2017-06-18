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
    [Route("api/scrapes")]
    [Authorize(Roles = "admin")]
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
        public async Task<IActionResult> Delete(int id)
        {
            await scrapeRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var scrapes = await scrapeRepository.ReadAllAsync();
            var scrapesDto = scrapes.Select(x => scrapeMapper.MapToScrapeDto(x));
            return Ok(scrapesDto);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var scrape = await scrapeRepository.ReadLatest();
            var scrapeDto = scrapeMapper.MapToScrapeDto(scrape);
            return Ok(scrapeDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var scrape = await scrapeRepository.ReadByIdAsync(id);
            if (scrape == null)
                return NotFound();

            var scrapeDto = scrapeMapper.MapToScrapeDto(scrape);
            return Ok(scrapeDto);
        }

        //[HttpPatch("{id}")]
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
        public async Task<IActionResult> Post([FromBody]ScrapeDto request)
        {
            var scrape = scrapeMapper.MapToScrape(request);
            await scrapeRepository.CreateAsync(scrape);
            var scrapeDto = scrapeMapper.MapToScrapeDto(scrape);
            return Created($"/api/scrapes/{scrape.Id}", scrapeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ScrapeDto request)
        {
            request.Id = id;
            var scrape = scrapeMapper.MapToScrape(request);
            await scrapeRepository.UpdateAsync(scrape);
            return Ok();
        }
    }
}
