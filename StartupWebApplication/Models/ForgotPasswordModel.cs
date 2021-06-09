using System.ComponentModel.DataAnnotations;

namespace PlantTrackerAPI.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Email { get; set; }
    }
}
