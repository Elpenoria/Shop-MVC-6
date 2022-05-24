using Microsoft.AspNetCore.Identity;

namespace Shop.Models
{
    public class ApplicationUser :IdentityUser
    {
        public bool isSeller { get; set; }
    }
}
