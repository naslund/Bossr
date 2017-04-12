using BossrApi.Models.Entities;
using BossrApi.Repositories.ScrapeRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/scrapes")]
    [Authorize(Roles = "admin")]
    public class ScrapesController : Controller
    {
        private readonly IScrapeRepository scrapeRepository;

        public ScrapesController(IScrapeRepository scrapeRepository)
        {
            this.scrapeRepository = scrapeRepository;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await scrapeRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var scrapes = await scrapeRepository.ReadAllAsync();
            return Ok(scrapes);
        }

        [HttpGet("{id}", Name = "GetScrape")]
        public async Task<IActionResult> Get(int id)
        {
            var scrape = await scrapeRepository.ReadAsync(id);
            if (scrape == null)
                return NotFound();

            return Ok(scrape);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var scrape = await scrapeRepository.ReadAsync(id);
            if (scrape == null)
                return NotFound();

            patch.ApplyTo(scrape);
            await scrapeRepository.UpdateAsync(scrape);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Scrape request)
        {
            await scrapeRepository.CreateAsync(request);
            var scrape = await scrapeRepository.ReadAsync(request.Id);
            return CreatedAtRoute("GetScrape", new { controller = "api/scrapes", id = scrape.Id }, scrape);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Scrape request)
        {
            request.Id = id;

            await scrapeRepository.UpdateAsync(request);
            return Ok();
        }
    }
}
