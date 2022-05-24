using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventHeader { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public IFormFile ImageUrlFile { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SellerId { get; set; }
        public int CategoryId { get; set; }
    }
}
