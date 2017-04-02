using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using BossrApi.Models.Requests;
using BossrApi.Repositories.UserRepository;
using BossrApi.Models.Dtos;
using BossrApi.Attributes;

namespace BossrApi.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
            await userRepository.CreateAsync(request.Username, request.Password);
            var user = await userRepository.ReadAsync(request.Username);
            var userDto = Mapper.Map<UserDto>(user);
            return CreatedAtRoute("GetUser", new { controller = "api/users", id = userDto.Id }, userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassword(string id, [FromBody]UserPutPasswordRequest request)
        {
            await userRepository.UpdatePasswordAsync(id, request.Password);
            return Ok();
        }
    }
}