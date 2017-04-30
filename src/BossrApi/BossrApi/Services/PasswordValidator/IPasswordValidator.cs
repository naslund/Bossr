using BossrLib.Models.Entities;

namespace BossrApi.Services
{
    public interface IPasswordValidator
    {
        bool IsPasswordValid(IUser user, string password);
    }
}