using AestheticShop.Extensions;
using AestheticShop.Helpers;
using AestheticShop.Models;
using AestheticShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Edit(int id)
        {

            var products = shopDbContext.Products.Find(id);

            ViewBag.categories = new SelectList(shopDbContext.Categories, "Id", "Name");

            var selectedTagIds = shopDbContext.ProductTags.Where(x => x.ProductId == id).Select(x => x.TagId);
            ViewBag.tags = new MultiSelectList(shopDbContext.Tags, "Id", "Name", selectedTagIds);

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile Image, int[] tags)
        {

            if (Image != null)
            {
                var path = await FileUploadHelper.UploadAsync(Image);
                product.ImageUrl = path;
            }

            //post.Date = DateTime.Now;

            shopDbContext.Products.Update(product);
            await shopDbContext.SaveChangesAsync();


            var postWithTags = shopDbContext.Products.Include(x => x.ProductTags).FirstOrDefault(x => x.Id == product.Id);
            int a = 0;
            shopDbContext.UpdateManyToMany(
                postWithTags.ProductTags,
                tags.Select(x => new ProductTag { ProductId = product.Id, TagId = x }),
                x => x.TagId
                );
            await shopDbContext.SaveChangesAsync();

            //blogDbContext.UpdateManyToMany();
            //var deletePostTag = blogDbContext.PostTags.Where(x => x.PostId == post.Id);
            //blogDbContext.PostTags.RemoveRange(deletePostTag);
            //blogDbContext.PostTags.AddRange(tags.Select(x => new PostTag { PostId = post.Id, TagId = x }));
            //await blogDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
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
            var products = shopDbContext.Products.Include(x => x.ProductTags).ThenInclude(x => x.Tag).Include(x => x.Category); ;
            var categories = shopDbContext.Categories;
            var tags = shopDbContext.Tags;

            var model = new IndexViewModel() { Categories = categories, Products = products,Tags=tags };
            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {

            var products = shopDbContext.Products
                .Include(x => x.ProductTags).ThenInclude(x => x.Tag)
                .Include(x => x.Category)
                .FirstOrDefault(products => products.Id == id);
            return View(products);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product,IFormFile Image, int[] tags)
        {
            try
            {
                product.ImageUrl = await FileUploadHelper.UploadAsync(Image);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (ModelState.IsValid)
            {
                TempData["status"] = "New product added!";
                await shopDbContext.Products.AddAsync(product);
                await shopDbContext.SaveChangesAsync();
                shopDbContext.ProductTags.AddRange(tags.Select(x => new ProductTag { ProductId = product.Id, TagId = x }));

                await shopDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            


            return RedirectToAction("Index");
        }
    }
}
