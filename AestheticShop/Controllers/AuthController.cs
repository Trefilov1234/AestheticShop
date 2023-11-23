using AestheticShop.Models;
using AestheticShop.Services;
using AestheticShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AestheticShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly ShopDbContext dbContext;
		private readonly IUserManager _userManager;

		public AuthController(ShopDbContext dbContext, IUserManager userManager) 
        {
            this.dbContext = dbContext;
			_userManager= userManager;

		}
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
		//public IActionResult Register(RegisterViewModel registerViewModel)
		//{
		//	if(ModelState.IsValid)
		//	{
		//		User newUser = new User()
		//		{
		//			Login = registerViewModel.Login,
		//			PasswordHash = SHA256Encriptor.Encript(registerViewModel.Password),
		//			IsAdmin = false
		//		};
		//		dbContext.Users.Add(newUser);
		//		dbContext.SaveChanges();
		//		return RedirectToAction("Login", "Auth");
		//	}
		//	else
		//	{
		//		return View(registerViewModel);
		//	}
		//}
		[HttpGet]
		public IActionResult Login()
		{

			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginViewModel loginViewModel)
		{
			if (_userManager.Login(loginViewModel.Login, loginViewModel.Password))
			{
				return RedirectToAction("Index", "Product");
			}

			ModelState.AddModelError("all", "Данные введены неверно");
			return View(loginViewModel);
		}
	}
}
