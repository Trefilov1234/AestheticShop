using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AestheticShop.Models;

namespace AestheticShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTagsController : Controller
    {
        private readonly ShopDbContext _context;

        public ProductTagsController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductTags
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.ProductTags.Include(p => p.Product).Include(p => p.Tag);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: Admin/ProductTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductTags == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTags
                .Include(p => p.Product)
                .Include(p => p.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }

            return View(productTag);
        }

        // GET: Admin/ProductTags/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Admin/ProductTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,TagId")] ProductTag productTag)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(productTag);
                await _context.SaveChangesAsync();
                
            //}
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productTag.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", productTag.TagId);
            //return View(productTag);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ProductTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductTags == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTags.FindAsync(id);
            if (productTag == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productTag.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", productTag.TagId);
            return View(productTag);
        }

        // POST: Admin/ProductTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,TagId")] ProductTag productTag)
        {
            if (id != productTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTagExists(productTag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productTag.ProductId);
            ViewData["TagId"] = new SelectList(_context.Tags, "Id", "Name", productTag.TagId);
            return View(productTag);
        }

        // GET: Admin/ProductTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductTags == null)
            {
                return NotFound();
            }

            var productTag = await _context.ProductTags
                .Include(p => p.Product)
                .Include(p => p.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productTag == null)
            {
                return NotFound();
            }

            return View(productTag);
        }

        // POST: Admin/ProductTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductTags == null)
            {
                return Problem("Entity set 'ShopDbContext.ProductTags'  is null.");
            }
            var productTag = await _context.ProductTags.FindAsync(id);
            if (productTag != null)
            {
                _context.ProductTags.Remove(productTag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTagExists(int id)
        {
          return (_context.ProductTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
