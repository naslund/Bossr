using AutoMapper;
using BossrApi.Repositories;
using BossrLib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            await scrapeRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var scrapes = await scrapeRepository.ReadAllAsync();
            var scrapesDto = Mapper.Map<List<ScrapeDto>>(scrapes);
            return Ok(scrapesDto);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var scrape = await scrapeRepository.ReadLatest();
            var scrapeDto = Mapper.Map<ScrapeDto>(scrape);
            return Ok(scrapeDto);
        }

        [HttpGet("{id}", Name = "GetScrape")]
        public async Task<IActionResult> Get(int id)
        {
            var scrape = await scrapeRepository.ReadByIdAsync(id);
            if (scrape == null)
                return NotFound();

            var scrapeDto = Mapper.Map<ScrapeDto>(scrape);
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
            var scrape = Mapper.Map<Scrape>(request);
            await scrapeRepository.CreateAsync(scrape);
            return CreatedAtRoute("GetScrape", new { controller = "api/scrapes", id = scrape.Id }, Mapper.Map<ScrapeDto>(scrape));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]ScrapeDto request)
        {
            request.Id = id;
            var scrape = Mapper.Map<Scrape>(request);
            await scrapeRepository.UpdateAsync(scrape);
            return Ok();
        }
    }
}
