using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductsController(IWebHostEnvironment hostEnvironment, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        

        public IActionResult YourCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCart =  _context.PendingCartItems.Where(x => x.UserId == userId).ToList();

            var pendingCartItems = new List<PendingCartItem>();
            foreach (var item in userCart)
            {
                Product product = _context.Products.FirstOrDefault(x => x.ProductId == item.ProductId);

                pendingCartItems.Add(new PendingCartItem
                {
                    Product = product,
                    Amount = item.Amount
                });

            }
            ViewData["TotalPrice"] = GetTotalPrice(pendingCartItems);

            return View(pendingCartItems);
        }

        public IActionResult Buy(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var useruserId = _userManager.GetUserAsync(User).Id;
            var currentCart = new List<PendingCart>(); 
            try
            {
                var currentPendingCart = _context.PendingCartItems.Where(x => x.UserId == userId).ToList();
                currentCart = currentPendingCart;
            }
            catch
            {
                var currentPendingCart = _context.PendingCartItems.Where(x => x.UserId == userId).ToList();
                currentCart = currentPendingCart;
            }
            if (currentCart.Any(x => x.ProductId == id))
            {
                var row = currentCart.FirstOrDefault(x => x.ProductId == id);
                row.Amount += 1;
            }
            else
            {
                var pendingCart = new PendingCart();
                pendingCart = new PendingCart
                {
                    Amount = 1,
                    ProductId = id,
                    UserId = userId
                };
                _context.PendingCartItems.Add(pendingCart);
            }


            _context.SaveChanges();

            return RedirectToAction("YourCart");
        }

        public decimal GetTotalPrice(List<PendingCartItem> items)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            decimal totalPrice = 0;

            foreach (var item in items)
            {
                totalPrice += item.Product.DiscountedPrice * item.Amount;
            }


            return totalPrice;
        }

        public IActionResult Order()
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
            //var order = new Order
            //{
            //    CustomerId = userId,
            //    OrderItem = productIds,
            //    OrderAmount = productAmounts
            //};

            //_context.Orders.Add(order);
            //_context.SaveChanges();

            return RedirectToAction("Index");
        }

        
        //public IActionResult EditProduct(int amount , int prodId)
        //{
        //    return View();
        //}

        public IActionResult RemoveItem(int item)
        {
            return View();
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            
            var products = await _context.Products.ToListAsync();
            //return _context.Movie != null ? 
            //            View(await _context.Movie.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            ViewData["Categories"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,MainImageFile,SecondImageFile,ThirdImageFile,Price,DiscountedPrice,InStock,CategoryId,EventId,SellerId")] Product product)
        { 
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string mainFile = Path.GetFileNameWithoutExtension(product.MainImageFile.FileName);
            string secondFile = Path.GetFileNameWithoutExtension(product.SecondImageFile.FileName);
            string thirdFile = Path.GetFileNameWithoutExtension(product.ThirdImageFile.FileName);


            string firstExtension = Path.GetExtension(product.MainImageFile.FileName);
            string secondExtension = Path.GetExtension(product.SecondImageFile.FileName);
            string thirdExtension = Path.GetExtension(product.ThirdImageFile.FileName);


            product.MainImage = mainFile = mainFile + DateTime.Now.ToString("yymmssfff") + firstExtension;
            product.SecondImage = secondFile = secondFile + DateTime.Now.ToString("yymmssfff") + secondExtension;
            product.ThirdImage = thirdFile = thirdFile + DateTime.Now.ToString("yymmssfff") + thirdExtension;

            string firstPath = Path.Combine(wwwRootPath + "/img/", mainFile);
            string secondPath = Path.Combine(wwwRootPath + "/img/", secondFile);
            string thirdPath = Path.Combine(wwwRootPath + "/img/", thirdFile);

            using (var fileStream = new FileStream(firstPath, FileMode.Create))
            {
                await product.MainImageFile.CopyToAsync(fileStream);
            }
            using (var fileStream = new FileStream(secondPath, FileMode.Create))
            {
                await product.SecondImageFile.CopyToAsync(fileStream);
            }
            using (var fileStream = new FileStream(thirdPath, FileMode.Create))
            {
                await product.ThirdImageFile.CopyToAsync(fileStream);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,MainImage,SecondImage,ThirdImage,Price,DiscountedPrice,InStock,CategoryId,EventId,SellerId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
