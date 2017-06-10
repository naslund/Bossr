using Bossr.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/states")]
    [Authorize(Roles = "admin")]
    public class StatesController : Controller
    {
        private readonly IStateCalculator stateCalculator;

        public StatesController(IStateCalculator stateCalculator)
        {
            this.stateCalculator = stateCalculator;
        }

        [HttpGet("{worldId}")]
        public async Task<IActionResult> Get(int worldId)
        {
            var states = await stateCalculator.GetStatesByWorldId(worldId);
            return Ok(states);
        }
    }
}
