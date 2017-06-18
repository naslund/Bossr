using Bossr.Lib.Models.Entities;

namespace Bossr.Api.Mappers
{
    public interface IUserMapper
    {
        UserDto MapToUserDto(IUser user);
    }

    public class UserMapper : IUserMapper
    {
        public UserDto MapToUserDto(IUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}
