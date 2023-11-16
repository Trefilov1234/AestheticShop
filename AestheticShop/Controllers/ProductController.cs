using AestheticShop.Extensions;
using AestheticShop.Helpers;
using AestheticShop.Models;
using AestheticShop.Services;
using AestheticShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AestheticShop.Controllers
{
    public class ProductController:Controller
    {
        private readonly ShopDbContext shopDbContext;
		private readonly IUserManager userManager;

		public ProductController(ShopDbContext shopDbContext,IUserManager userManager)
        {
            this.shopDbContext = shopDbContext;
			this.userManager = userManager;
		}
        [HttpGet]
        public IActionResult Reset()
        {

            var products = shopDbContext.Products;


            return RedirectToAction("Index", products);
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
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var products = shopDbContext.Products.Find(id);
            return View(products);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmedDelete(int id)
        {
            var products = shopDbContext.Products.Find(id);
            shopDbContext.Products.Remove(products);
            await shopDbContext.SaveChangesAsync();
            TempData["status"] = "Product deleted!";
            return RedirectToAction("Index");
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
        public IActionResult Index(int? categoryId = null, int? tagId = null, int page = 1)
        {
			ViewBag.UserName = userManager.CurrentUser?.Login ?? "Guest";
			var products = shopDbContext.Products.Include(x => x.ProductTags).ThenInclude(x => x.Tag).Include(x => x.Category).OrderByDescending(x => x.Id);

            if (categoryId != null)
            {
                products = (IOrderedQueryable<Product>)products.Where(x => x.CategoryId == categoryId);
            }

            if (tagId != null)
            {
                //var postIdsByTag = blogDbContext.PostTags.Where(pt => pt.TagId == tagId).Select(x=>x.PostId);
                //posts = (IOrderedQueryable<Post>)posts.Where(p =>postIdsByTag.Contains(p.Id));
                products = (IOrderedQueryable<Product>)products.Where(x => x.ProductTags.Any(x => x.TagId == tagId));
            }
            var model = new IndexViewModel();
            int totalPages=(int)Math.Ceiling(products.Count() / (double)model.LimitPage);
            products = (IOrderedQueryable<Product>)products.Skip((page-1)* model.LimitPage).Take(model.LimitPage);


            model.Categories = shopDbContext.Categories;
            model.Products = products;
            model.Tags = shopDbContext.Tags;
            model.RecentProducts = shopDbContext.Products.OrderByDescending(x => x.Id).Take(model.LimitPage);
            model.CurrentPage = page;
            model.TotalPages = totalPages;
            model.SelectedCategoryId = categoryId;
            model.SelectedTagId = tagId;
            
            //return View("Privacy", model);
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
            //if (ModelState.IsValid)
            //{
                TempData["status"] = "New product added!";
                await shopDbContext.Products.AddAsync(product);
                await shopDbContext.SaveChangesAsync();
                shopDbContext.ProductTags.AddRange(tags.Select(x => new ProductTag { ProductId = product.Id, TagId = x }));

                await shopDbContext.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            return RedirectToAction("Index");
        }
    }
}
