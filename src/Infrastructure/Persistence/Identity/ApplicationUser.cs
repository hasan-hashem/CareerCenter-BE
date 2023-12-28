using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstNmae { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}
