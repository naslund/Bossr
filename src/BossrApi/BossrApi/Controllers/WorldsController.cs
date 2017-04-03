using AutoMapper;
using BossrApi.Attributes;
using BossrApi.Models.Dtos;
using BossrApi.Models.Requests;
using BossrApi.Repositories.WorldRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/worlds")]
    [Authorize(Roles = "admin")]
    public class WorldsController : Controller
    {
        private readonly IWorldRepository worldRepository;
        public WorldsController(IWorldRepository worldRepository)
        {
            this.worldRepository = worldRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var worlds = await worldRepository.ReadAsync();
            var worldsDto = worlds.Select(x => Mapper.Map<WorldDto>(x));
            return Ok(worldsDto);
        }

        [HttpGet("{id}", Name = "GetWorld")]
        public async Task<IActionResult> Get(int id)
        {
            var world = await worldRepository.ReadAsync(id);
            if (world == null)
                return NotFound();
            var worldDto = Mapper.Map<WorldDto>(world);
            return Ok(worldDto);
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "World name not available.")]
        public async Task<IActionResult> Post([FromBody]WorldPostRequest request)
        {
            await worldRepository.CreateAsync(request.Name);
            var world = await worldRepository.ReadAsync(request.Name);
            var worldDto = Mapper.Map<WorldDto>(world);
            return CreatedAtRoute("GetWorld", new { controller = "api/worlds", id = worldDto.Id }, worldDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await worldRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]WorldPutRequest request)
        {
            await worldRepository.UpdateNameAsync(id, request.Name);
            return Ok();
        }
    }
}
