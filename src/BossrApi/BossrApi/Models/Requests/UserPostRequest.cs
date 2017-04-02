using System.ComponentModel.DataAnnotations;

namespace BossrApi.Models.Requests
{
    public class UserPostRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Only alphanumeric characters (A-Z, a-z, 0-9) and underscores are allowed.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}