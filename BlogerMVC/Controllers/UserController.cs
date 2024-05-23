using BlogerMVC.Core.Models;
using BlogerMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogerMVC.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
			_userManager = userManager;
        }

        public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(UserLoginVm vm)
		{
			if (!ModelState.IsValid)
				return View();

			AppUser user= await _userManager.FindByNameAsync(vm.UserName);

			if(user == null)
			{
				ModelState.AddModelError("", "UserName or Password is not valid!");
				return View();
			}

			var result= await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsPersistent, false);

			if (!result.Succeeded)
			{
				ModelState.AddModelError("", "UserName or Password is not valid!");
				return View();
			}

			return RedirectToAction("Index", "Home");

		}

		public async Task<IActionResult> SignOut()
		{
		    await	_signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}
	}
}
