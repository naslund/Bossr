using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/worlds")]
    [Authorize(Roles = "admin")]
    public class WorldsController : Controller
    {

    }
}
