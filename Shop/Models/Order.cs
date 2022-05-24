namespace Shop.Models
{
    public class Order
    {
        public int OrderId { get; set; }


        public List<int> OrderItem = new List<int>();
        public List<int> OrderAmount = new List<int>();
        public string CustomerId { get; set; }

        //    public static ShoppingCart GetCart(IServiceProvider services)
        //    {
        //        ISession session = services.GetRequiredService<IHttpContextAccessor>()?
        //            .HttpContext.Session;
        //        var context = services.GetService<ApplicationDbContext>();
        //        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        //        session.SetString("CartId", cartId);
        //        return new ShoppingCart(context) { ShoppingCartId = cartId };
        //    }


        //public void AddToCart(Product good, int amount)
        //{
        //    var shoppingCartItem =
        //            _context.ShoppingCartItems.SingleOrDefault(
        //                  s => s.Good.GoodId == good.GoodId && s.ShoppingCartId == ShoppingCartId);
        //    if (shoppingCartItem == null)
        //    {
        //        shoppingCartItem = new PendingCarts
        //        {
        //            ShoppingCartId = ShoppingCartId,
        //            Good = good,
        //            Amount = 1
        //        };
        //        _context.ShoppingCartItems.Add(shoppingCartItem);
        //    }
        //    else
        //    {
        //        shoppingCartItem.Amount++;
        //    }
        //    _context.SaveChanges();
        //}

        //    public List<ShoppingCartItem> GetShoppingCartItems()
        //    {
        //        return _appDbContext.ShoppingCartItems
        //                        .Where(c => c.ShoppingCartId == ShoppingCartId)
        //                        .Include(s => s.Good)
        //                        .ToList();
        //    }


        //    public void ClearCart()
        //    {
        //        var cartItems = _appDbContext
        //            .ShoppingCartItems
        //            .Where(cart => cart.ShoppingCartId == ShoppingCartId);
        //        _appDbContext.ShoppingCartItems.RemoveRange(cartItems);
        //        _appDbContext.SaveChanges();
        //    }


        //    public decimal GetShoppingCartTotal()
        //    {
        //        var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
        //              .Select(c => c.Good.Price * c.Amount).Sum();
        //        return total;
        //    }

        //}
    }
}
