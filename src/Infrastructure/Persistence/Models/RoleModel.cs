using System.ComponentModel.DataAnnotations;

namespace Persistence.Models
{
    public class RoleModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
