using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateOrder()
        {
            

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCart = _context.PendingCartItems.Where(x => x.UserId == userId).ToList();

            List<int> productIds = new List<int>();
            List<int> productAmounts = new List<int>();

            foreach (var item in userCart)
            {
                productIds.Add(item.ProductId);
                productAmounts.Add(item.Amount);
            }
            var order = new Order
            {
                CustomerId = userId,
                OrderItem = productIds,
                OrderAmount = productAmounts
            };

            _context.Orders.Add(order);
            foreach(var cart in userCart)
            {
                _context.PendingCartItems.Remove(cart);
            }
            _context.SaveChanges();


            return RedirectToAction("OkReturn");
        }

        public async Task<IActionResult> RemoveOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myCartItems = _context.PendingCartItems.Where(x => x.UserId == userId).ToList();

            foreach (var item in  myCartItems)
            {
                _context.PendingCartItems.Remove(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("OkReturn");
        }

        public IActionResult OkReturn()
        {
            return View();
        }
        
       
    }
}
