using System.ComponentModel.DataAnnotations;

namespace BossrApi.Models.Requests
{
    public class WorldPostRequest
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
