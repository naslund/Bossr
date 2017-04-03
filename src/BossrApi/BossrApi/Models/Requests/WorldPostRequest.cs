using System.ComponentModel.DataAnnotations;

namespace BossrApi.Models.Requests
{
    public class WorldPostRequest
    {
        [Required]
        [StringLength(ValidationRules.WorldNameMaxLength)]
        public string Name { get; set; }
    }
}
