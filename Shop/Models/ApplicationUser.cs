using Microsoft.AspNetCore.Identity;

namespace Shop.Models
{
    public class ApplicationUser :IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public string Qyteti { get; set; }
        

    }
}
