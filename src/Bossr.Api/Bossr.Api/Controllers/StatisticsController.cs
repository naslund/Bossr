using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/statistics")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticRepository statisticRepository;

        public StatisticsController(IStatisticRepository statisticRepository)
        {
            this.statisticRepository = statisticRepository;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteStatistics")]
        public async Task<IActionResult> Delete(int id)
        {
            await statisticRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadStatistics")]
        public async Task<IActionResult> Get()
        {
            var statistics = await statisticRepository.ReadAllAsync();
            return Ok(statistics);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadStatistics")]
        public async Task<IActionResult> Get(int id)
        {
            var statistic = await statisticRepository.ReadByIdAsync(id);
            if (statistic == null)
                return NotFound();

            return Ok(statistic);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "UpdateStatistics")]
        public async Task<IActionResult> Patch(int id, [FromBody]JsonPatchDocument patch)
        {
            var statistic = await statisticRepository.ReadByIdAsync(id);
            if (statistic == null)
                return NotFound();

            patch.ApplyTo(statistic);
            await statisticRepository.UpdateAsync(statistic);
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "CreateStatistics")]
        public async Task<IActionResult> Post([FromBody]Statistic request)
        {
            await statisticRepository.CreateAsync(request);
            var statistic = await statisticRepository.ReadByIdAsync(request.Id);
            return Created($"/api/statistics/{statistic.Id}", statistic);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "UpdateStatistics")]
        public async Task<IActionResult> Put(int id, [FromBody]Statistic request)
        {
            request.Id = id;

            await statisticRepository.UpdateAsync(request);
            return Ok();
        }
    }
}
