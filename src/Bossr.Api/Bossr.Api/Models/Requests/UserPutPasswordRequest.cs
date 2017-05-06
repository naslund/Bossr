using System.ComponentModel.DataAnnotations;

namespace Bossr.Api.Models.Requests
{
    public class UserPutPasswordRequest
    {
        [Required]
        [StringLength(ValidationRules.UserPasswordMaxLength, MinimumLength = ValidationRules.UserPasswordMinLength)]
        public string Password { get; set; }
    }
}