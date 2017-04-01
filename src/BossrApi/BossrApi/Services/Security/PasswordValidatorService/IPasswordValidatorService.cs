using BossrApi.Models.Interfaces;

namespace BossrApi.Services.Security.PasswordValidatorService
{
    public interface IPasswordValidatorService
    {
        bool IsPasswordValid(IUser user, string password);
    }
}
