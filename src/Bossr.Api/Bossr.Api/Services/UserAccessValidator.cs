using Microsoft.AspNetCore.Http;

namespace Bossr.Api.Services
{
    public interface IUserAccessValidator
    {
        bool IsCurrentUserAllowedToAccessUserResources(int resourceUserId);
    }

    public class UserAccessValidator : IUserAccessValidator
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserIdentityReader userIdentityReader;

        public UserAccessValidator(IHttpContextAccessor httpContextAccessor, IUserIdentityReader userIdentityReader)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userIdentityReader = userIdentityReader;
        }

        public bool IsCurrentUserAllowedToAccessUserResources(int resourceUserId)
        {
            var currentUser = httpContextAccessor.HttpContext.User;
            var currentUserId = userIdentityReader.GetUserId(currentUser);

            return currentUserId == resourceUserId;
        }
    }
}
