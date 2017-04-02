using System.ComponentModel.DataAnnotations;

namespace BossrApi.Models.Requests
{
    public class UserPutPasswordRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
