using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class LogInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
