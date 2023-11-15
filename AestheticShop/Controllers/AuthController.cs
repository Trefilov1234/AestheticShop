using AestheticShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace AestheticShop.Controllers
{
    public class AuthController : Controller
    {
        private readonly ShopDbContext dbContext;

        public AuthController(ShopDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Regiser()
        {
            return View();
        }
    }
}
