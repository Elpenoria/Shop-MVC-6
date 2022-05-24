using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public IFormFile ImageUrl { get; set; }

    }
}
