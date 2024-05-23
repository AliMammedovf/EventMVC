using BlogerMVC.Core.Models;
using BlogerMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogerMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser()
            {
                FullName = "Eli Memmedov",
                UserName = "SuperAdmin"
            };

            await _userManager.CreateAsync(admin, "Admin123@");
            await _userManager.AddToRoleAsync(admin, "SuperAdmin");

            return Ok("Admin yarandi!");
        }

        public async Task<IActionResult> CreateRoles()
        {
            IdentityRole role = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);

            return Ok("Rollar yarandi!");
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm vm)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser admin= await _userManager.FindByNameAsync(vm.UserName);

            if(admin == null)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, vm.Password, vm.IsPersistent, false);
            
                
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "UserName or Password is not valid!");
                return View();
            }

            return RedirectToAction("Index", "Dashboard");


        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
