using BossrApi.Models.Interfaces;

namespace BossrApi.Services.PasswordValidator
{
    public interface IPasswordValidator
    {
        bool IsPasswordValid(IUser user, string password);
    }
}
