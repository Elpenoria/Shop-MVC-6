namespace Shop.Models
{
    public class PendingCart
    {
        public int PendingCartId { get; set; }
        public int Amount { get; set; }
        public string? UserId { get; set; }
        public int ProductId { get; set; }
    }
}
