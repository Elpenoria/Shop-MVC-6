using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display( Name = "Product Description")]
        public string Description { get; set; }
        public string MainImage { get; set; }
        [NotMapped]
        public IFormFile MainImageFile { get; set; }
        public string? SecondImage { get; set; }
        [NotMapped]
        public IFormFile? SecondImageFile { get; set; }
        public string? ThirdImage { get; set; }
        [NotMapped]
        public IFormFile? ThirdImageFile { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
        [Required]
        public int InStock { get; set; }
        public int CategoryId { get; set; }
        public int? EventId { get; set; }
        public string? SellerId { get; set; }
        //Krijo lidhjen ketu
    }
}
