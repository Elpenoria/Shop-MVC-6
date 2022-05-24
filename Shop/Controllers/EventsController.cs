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

    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public EventsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = _context.Events.ToList();
            foreach (var ev in events)
            {
                if (ev.EndDate > DateTime.Now)
                {
                    var seller = ev.SellerId;

                    var sellerProducts = _context.Products.Where(s => s.SellerId == ev.SellerId && s.CategoryId == ev.CategoryId);

                    foreach (var product in sellerProducts)
                    {
                        product.DiscountedPrice = product.Price - ( product.Price * ev.Discount)/100;
                    }
                    

                }

            }
            
            _context.SaveChanges();

            return _context.Events != null ?
                        View(await _context.Events.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Events'  is null.");
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            ViewData["Categories"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventHeader,Description,Discount,ImageUrlFile,StartDate,EndDate,SellerId,CategoryId")] Event @event)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string mainFile = Path.GetFileNameWithoutExtension(@event.ImageUrlFile.FileName);

            string imageExtension = Path.GetExtension(@event.ImageUrlFile.FileName);

            @event.ImageUrl = mainFile = mainFile + DateTime.Now.ToString("yymmssfff") + imageExtension;

            string imagePath = Path.Combine(wwwRootPath + "/img/", mainFile);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await @event.ImageUrlFile.CopyToAsync(fileStream);
            }
            _context.Add(@event);
            await _context.SaveChangesAsync();


            var events = _context.Events.ToList();
            foreach (var ev in events){
                if(ev.EndDate > DateTime.Now)
                {
                    var seller = ev.SellerId;

                    var sellerProducts = _context.Products.Where(s => s.SellerId == ev.SellerId && s.CategoryId == ev.CategoryId);
                    
                    foreach(var product in sellerProducts)
                    {
                        product.DiscountedPrice = product.Price * (1 - ev.Discount);
                    }
                    

                }

            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventHeader,Description,Discount,ImageUrl,StartDate,EndDate,SellerId,CategoryId")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
