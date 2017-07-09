using Bossr.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("states")]
    public class StatesController : Controller
    {
        private readonly IStateCalculator stateCalculator;

        public StatesController(IStateCalculator stateCalculator)
        {
            this.stateCalculator = stateCalculator;
        }

        [HttpGet("{worldId}")]
        [Authorize(Policy = "ReadStates")]
        public async Task<IActionResult> Get(int worldId)
        {
            var states = await stateCalculator.GetStatesByWorldId(worldId);
            return Ok(states);
        }
    }
}
