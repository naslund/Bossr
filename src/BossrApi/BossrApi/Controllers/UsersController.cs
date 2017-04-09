using AutoMapper;
using BossrApi.Attributes;
using BossrApi.Models.Dtos;
using BossrApi.Models.Requests;
using BossrApi.Repositories.UserRepository;
using BossrApi.Services.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BossrApi.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IUserRepository userRepository;
        public UsersController(IUserRepository userRepository, IUserManager userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await userRepository.ReadAsync();
            var usersDto = users.Select(x => Mapper.Map<UserDto>(x));
            return Ok(usersDto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await userRepository.ReadAsync(id);
            if (user == null)
                return NotFound();

            var userDto = Mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Username not available.")]
        public async Task<IActionResult> Post([FromBody]UserPostRequest request)
        {
            await userManager.CreateUserAsync(request.Username, request.Password);

            var user = await userRepository.ReadAsync(request.Username);
            var userDto = Mapper.Map<UserDto>(user);
            return CreatedAtRoute("GetUser", new { controller = "api/users", id = userDto.Id }, userDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassword(int id, [FromBody]UserPutPasswordRequest request)
        {
            await userManager.UpdatePasswordAsync(id, request.Password);
            return Ok();
        }
    }
}