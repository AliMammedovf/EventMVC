using BlogerMVC.Core.Models;
using BlogerMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogerMVC.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterVm vm)
		{
			if (!ModelState.IsValid)
				return View();

			AppUser user = null;

			user= await _userManager.FindByNameAsync(vm.UserName);

			if( user is not null )
			{
				ModelState.AddModelError("UserName", "UserName already exsist!");
				return View();
			}

			user= await _userManager.FindByEmailAsync(vm.Email);
			if( user is not null )
			{
				ModelState.AddModelError("Email", "Email already exsist!");
				return View();
			}

			user = new AppUser()
			{
				
				Email = vm.Email,
				UserName = vm.UserName,
			};

			var result= await _userManager.CreateAsync(user, vm.Password);

			if( !result.Succeeded )
			{
				foreach( var item in result.Errors )
				{
					ModelState.AddModelError("", item.Description);
					return View();
				}
			}

			 await _userManager.AddToRoleAsync(user, "Member");

			return RedirectToAction("Login");
		}
	}
}
