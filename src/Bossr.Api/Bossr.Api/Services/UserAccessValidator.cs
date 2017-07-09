namespace Bossr.Api.Services
{
    public interface IUserAccessValidator
    {
        bool IsCurrentUserAllowedToAccessUserResources(int currentUserId, int resourceUserId);
    }

    public class UserAccessValidator : IUserAccessValidator
    {
        public bool IsCurrentUserAllowedToAccessUserResources(int currentUserId, int resourceUserId)
        {
            return currentUserId == resourceUserId;
        }
    }
}
