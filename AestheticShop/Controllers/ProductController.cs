using AestheticShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace AestheticShop.Controllers
{
    public class ProductController:Controller
    {
        private readonly ShopDbContext shopDbContext;
        public ProductController(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = shopDbContext.Products;
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            if (ModelState.IsValid)
            {
                TempData["status"] = "New product added!";
                await shopDbContext.Products.AddAsync(product);
                await shopDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
