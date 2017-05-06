using Bossr.Lib.Models.Entities;

namespace Bossr.Api.Services
{
    public interface IPasswordValidator
    {
        bool IsPasswordValid(IUser user, string password);
    }
}