using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    
    public class ProductCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductCategoriesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: ProductCategories
        
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userCart = _context.PendingCartItems.Where(x => x.UserId == userId).ToList();


            HttpContext.Session.SetInt32("CartItems", userCart.Count());

            return _context.ProductCategories != null ?
                        View(await _context.ProductCategories.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ProductCategories'  is null.");
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductCategoryId,Name,Description,ImageUrl")] ProductCategory productCategory)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string mainFile = Path.GetFileNameWithoutExtension(productCategory.ImageUrl.FileName);

            string imageExtension = Path.GetExtension(productCategory.ImageUrl.FileName);

            productCategory.ImageName = mainFile = mainFile + DateTime.Now.ToString("yymmssfff") + imageExtension;

            string imagePath = Path.Combine(wwwRootPath + "/img/", mainFile);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await productCategory.ImageUrl.CopyToAsync(fileStream);
            }

            _context.Add(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductCategoryId,Name,Description,ImageName")] ProductCategory productCategory)
        {
            if (id != productCategory.ProductCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.ProductCategoryId))
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
            return View(productCategory);
        }

        // GET: ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductCategories == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(m => m.ProductCategoryId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductCategories'  is null.");
            }
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory != null)
            {
                _context.ProductCategories.Remove(productCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return (_context.ProductCategories?.Any(e => e.ProductCategoryId == id)).GetValueOrDefault();
        }
    }
}
