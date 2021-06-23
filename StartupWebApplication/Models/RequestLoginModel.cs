using System.ComponentModel.DataAnnotations;

namespace PlantTrackerAPI.Models
{
    public class RequestLoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
