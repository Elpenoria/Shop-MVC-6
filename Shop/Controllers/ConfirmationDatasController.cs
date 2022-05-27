using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    
    public class ConfirmationDatasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmationDatasController(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ConfirmationDatas
        
        public async Task<IActionResult> Index()
        {
            //return _context.Movie != null ? 
            //            View(await _context.Movie.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.ConfirmationData'  is null.");
            return View();
        }

        // GET: ConfirmationDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConfirmationData == null)
            {
                return NotFound();
            }

            var confirmationData = await _context.ConfirmationData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (confirmationData == null)
            {
                return NotFound();
            }

            return View(confirmationData);
        }

        [Authorize(Roles = "User")]
        // GET: ConfirmationDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConfirmationDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(ConfirmationData confirmationData)
        {
            
            confirmationData.CostumerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DateTime now = DateTime.Now;
            confirmationData.CreatedDate = now;
            confirmationData.Email = _userManager.GetUserName(User);
            _context.Add(confirmationData);
            await _context.SaveChangesAsync();
                
            return RedirectToAction("CreateOrder", "Orders");
        }

        // GET: ConfirmationDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConfirmationData == null)
            {
                return NotFound();
            }

            var confirmationData = await _context.ConfirmationData.FindAsync(id);
            if (confirmationData == null)
            {
                return NotFound();
            }
            return View(confirmationData);
        }

        // POST: ConfirmationDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Phone,PostalCode,CardNumber,CreatedDate,Email,CostumerId")] ConfirmationData confirmationData)
        {
            if (id != confirmationData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(confirmationData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfirmationDataExists(confirmationData.Id))
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
            return View(confirmationData);
        }

        // GET: ConfirmationDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConfirmationData == null)
            {
                return NotFound();
            }

            var confirmationData = await _context.ConfirmationData
                .FirstOrDefaultAsync(m => m.Id == id);
            if (confirmationData == null)
            {
                return NotFound();
            }

            return View(confirmationData);
        }

        // POST: ConfirmationDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConfirmationData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ConfirmationData'  is null.");
            }
            var confirmationData = await _context.ConfirmationData.FindAsync(id);
            if (confirmationData != null)
            {
                _context.ConfirmationData.Remove(confirmationData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfirmationDataExists(int id)
        {
          return (_context.ConfirmationData?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
