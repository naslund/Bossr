using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using System.Linq;
using BossrApi.Models.Responses;
using BossrApi.Models.Requests;
using BossrApi.Repositories.UserRepository;
using BossrApi.Models.Dtos;

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
            var userDto = Mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserPostRequest request)
        {
            try
            {
                await userRepository.CreateAsync(request.Username, request.Password);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                var error = new MessageResponse { Message = "Username not available." };
                return BadRequest(error);
            }

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