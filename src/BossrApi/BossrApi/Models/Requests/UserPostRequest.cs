using System.ComponentModel.DataAnnotations;

namespace BossrApi.Models.Requests
{
    public class UserPostRequest
    {
        [Required]
        [StringLength(ValidationRules.UserUsernameMaxLength, MinimumLength = ValidationRules.UserUsernameMinLength)]
        [RegularExpression(ValidationRules.UserUsernameRegex, ErrorMessage = ValidationRules.UserUsernameRegexErrorMessage)]
        public string Username { get; set; }

        [Required]
        [StringLength(ValidationRules.UserPasswordMaxLength, MinimumLength = ValidationRules.UserPasswordMinLength)]
        public string Password { get; set; }
    }
}