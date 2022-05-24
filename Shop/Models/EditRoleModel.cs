using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class EditRoleModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
