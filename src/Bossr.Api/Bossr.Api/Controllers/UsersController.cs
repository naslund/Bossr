using AutoMapper;
using Bossr.Api.Attributes;
using Bossr.Api.Models.Requests;
using Bossr.Api.Repositories;
using Bossr.Api.Services;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
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
            await userRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await userRepository.ReadAllAsync();
            var usersDto = users.Select(x => Mapper.Map<UserDto>(x));
            return Ok(usersDto);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await userRepository.ReadByIdAsync(id);
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

            var user = await userRepository.ReadByUsername(request.Username);
            var userDto = Mapper.Map<UserDto>(user);
            return CreatedAtRoute("GetUser", new { controller = "api/users", id = userDto.Id }, userDto);
        }

        [HttpPut("{id}/password")]
        public async Task<IActionResult> PutPassword(int id, [FromBody]UserPutPasswordRequest request)
        {
            await userManager.UpdatePasswordAsync(id, request.Password);
            return Ok();
        }
    }
}