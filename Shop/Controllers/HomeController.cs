using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var itemsInCart = _context.PendingCartItems.Where(x => x.UserId == userId).Count();
            HttpContext.Session.SetInt32("CartItems", itemsInCart);
            ViewData["CartCount"] = HttpContext.Session.GetInt32("CartItems");

            return RedirectToAction("Index" , "Products");
        }
       
        public IActionResult Error404()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}