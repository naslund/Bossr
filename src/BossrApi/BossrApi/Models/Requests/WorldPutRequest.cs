using System.ComponentModel.DataAnnotations;

namespace BossrApi.Models.Requests
{
    public class WorldPutRequest
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
