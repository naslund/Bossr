using Bossr.Api.Attributes;
using Bossr.Api.Mappers;
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
    public class UsersController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IUserRepository userRepository;
        private readonly IUserMapper userMapper;

        public UsersController(IUserRepository userRepository, IUserManager userManager, IUserMapper userMapper)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.userMapper = userMapper;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteUsers")]
        public async Task<IActionResult> Delete(int id)
        {
            await userRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ReadUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await userRepository.ReadAllAsync();
            var usersDto = users.Select(x => userMapper.MapToUserDto(x));
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadUsers")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await userRepository.ReadByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = userMapper.MapToUserDto(user);
            return Ok(userDto);
        }

        [HttpPost]
        [SqlExceptionFilter(2627, "Username not available.")]
        [Authorize(Policy = "CreateUsers")]
        public async Task<IActionResult> Post([FromBody]UserPostRequest request)
        {
            await userManager.CreateUserAsync(request.Username, request.Password);

            var user = await userRepository.ReadByUsername(request.Username);
            var userDto = userMapper.MapToUserDto(user);
            return Created($"/api/users/{userDto.Id}", userDto);
        }

        [HttpPut("{id}/password")]
        [Authorize(Policy = "UpdateUsers")]
        public async Task<IActionResult> PutPassword(int id, [FromBody]UserPutPasswordRequest request)
        {
            await userManager.UpdatePasswordAsync(id, request.Password);
            return Ok();
        }
    }
}