using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Models;

namespace Shop.Controllers
{
    public class Admin1Controller : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        public RoleManager<IdentityRole> RoleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }
       
        public Admin1Controller(UserManager<ApplicationUser> userManager,
       RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;

        }

        public IActionResult Index()
        {
            var users = UserManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Qyteti = user.Qyteti,
                Role = string.Join(",", UserManager.GetRolesAsync(user).Result.ToArray())
            }).ToList();
            return View(users);

        }
        [HttpGet]
        public async Task<IActionResult> ShowEdit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User nuk gjendet");
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                BirthDate = user.BirthDate,
                Qyteti = user.Qyteti,

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest("User nuk gjendet" + model.Id);
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.BirthDate = model.BirthDate;
                user.Qyteti = model.Qyteti;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                await UserManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User nuk gjendet");
            }
            var model = new UserViewModel
            {
                UserName = user.UserName
            };
            var roles = RoleManager.Roles;
            ViewBag.Roles = new SelectList(roles.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserRole(UserViewModel u)
        {
            var name = Convert.ToString(await RoleManager.FindByIdAsync(u.Role));
            var user = await UserManager.FindByIdAsync(u.Id);
            if (user == null)
            {
                return BadRequest("User nuk ekziston" + name);
            }
            await UserManager.AddToRoleAsync(user, name);
            return RedirectToAction("Index");

        }



    }
}
