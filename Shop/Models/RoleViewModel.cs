using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
