using AestheticShop.Helpers;
using AestheticShop.Models;
using AestheticShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.categories = new SelectList(shopDbContext.Categories, "Id", "Name");
            ViewBag.tags = new MultiSelectList(shopDbContext.Tags, "Id", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var products = shopDbContext.Products;
            var products = shopDbContext.Products;
            var categories = shopDbContext.Categories;
            var tags = shopDbContext.Tags;

            var model = new IndexViewModel() { Categories = categories, Products = products,Tags=tags };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product,IFormFile Image)
        {
            try
            {
                product.ImageUrl = await FileUploadHelper.UploadAsync(Image);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //if(Image!=null)
            //{
            //    var filename = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";
            //    using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
            //    await Image.CopyToAsync(fs);
            //    product.ImageUrl = @$"/uploads/{filename}";
            //}
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
