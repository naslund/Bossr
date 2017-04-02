using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BossrApi.Controllers
{
    [Route("api/worlds")]
    [Authorize(Roles = "admin")]
    public class WorldsController : Controller
    {

    }
}
