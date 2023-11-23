using AestheticShop.Areas.Admin.Services;
using AestheticShop.Models;
using AestheticShop.Models.Identity;
using AestheticShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AestheticShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopDbContext _shopDbContext;
        //UserManager<User> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        public SignInManager<AppUser> signInManager { get; set; }
        public UserManager<AppUser> userManager { get; private set; }

        private readonly MailSenderService mailSenderService;
        public AccountController(ShopDbContext _userDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> singManager,
            MailSenderService mailSenderService, RoleManager<IdentityRole> roleManager)

        {
            this._shopDbContext = _userDbContext;
            this.userManager = userManager;
            this.signInManager = singManager;
            this.mailSenderService = mailSenderService;
            _roleManager = roleManager;

        }
        public IActionResult Register()
        {
            return View();
        }





        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Id=Guid.NewGuid().ToString(),
                    UserName = registerViewModel.UserName,
                    FullName = registerViewModel.FullName,
                    Age = registerViewModel.Age,
                    Email=registerViewModel.Email,
                };
                var result = await userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    ////string str = $"http://localhost:19676/Account/ConfirmEmail?userName={user.UserName}&token={token}";
                    //var URL = Url.Action("ConfirmEmail",
                    //                    "Account",
                    //                    new { userId = user.Id, token = token },
                    //                    protocol: HttpContext.Request.Scheme);

                    //mailSenderService.Send(registerViewModel.Email, "Please confirm your registration", URL);
                    // await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, $"{err.Description}");

                    }


                }
            }
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            var user = await userManager.FindByNameAsync(loginViewModel.Login);
            
            if (user != null)
            {
                var role = await userManager.GetRolesAsync(user);
                //if (await userManager.IsEmailConfirmedAsync(user))
                //{
                if (await userManager.CheckPasswordAsync(user, loginViewModel.Password))
                {
                    await signInManager.SignInAsync(user, loginViewModel.RememberMe);
                    if(role.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home",new { area="Admin" });
                    }
               
                    //if (!string.IsNullOrWhiteSpace(returnUrl))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("Login", "Incorrect Username or Password");
                }

                //}
                //else
                //{
                //    ModelState.AddModelError("Login", "Please confirm your email");
                //}


            }
            else
            {
                ModelState.AddModelError("Login", "Incorrect Username or Password");
            }


            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Email confirmed";
                }
                else
                {
                    ViewBag.Message = "Error";
                }
            }
            else
            {
                ViewBag.Message = "Error";
            }

            return View();

        }
    }
}
