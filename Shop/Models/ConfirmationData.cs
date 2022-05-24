using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class ConfirmationData
    {
        // emer mbiemer nrteli  , postalcode nrKartes 
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName  { get; set; }
        [Required]
        [Display(Name = "Phone to see your order confirmation")]
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [Required]
        public int PostalCode { get; set; }
        [Required]
        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }

        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Email to track package")]

        public string? Email { get; set; }

        public string? CostumerId { get; set; }
    }
}
