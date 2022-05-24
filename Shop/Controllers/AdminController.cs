using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityResult sellerResult = await _roleManager.CreateAsync(new IdentityRole("Seller"));
            IdentityResult userResult = await _roleManager.CreateAsync(new IdentityRole("User"));

            if(sellerResult.Succeeded && userResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
