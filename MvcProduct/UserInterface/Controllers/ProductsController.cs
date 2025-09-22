using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcProduct.ApplicationCore.Interfaces;
using MvcProduct.Infrastructure.Data;
using MvcProduct.ApplicationCore.Services;
using MvcProduct.UserInterface.Models;

namespace MvcProduct.UserInterface.Controllers
{
    [Route("Products")]
    public class ProductsController : Controller
    {
        private readonly MvcProductContext _context;
        private readonly ILogger<ProductsController> _logger;
        private readonly IDaysPurchased _daysPurchased;
        public ProductsController(MvcProductContext context, ILogger<ProductsController> logger, IDaysPurchased daysPurchased)
        {
            _context = context;
            _logger = logger;
            _daysPurchased = daysPurchased;
        }

        
        [HttpGet] // GET: /Products
        public async Task<IActionResult> Index()
        {
            return View("~/UserInterface/Views/Products/Index.cshtml", await _context.Products.ToListAsync());
        }

        // GET: /Products/Details/1
        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            _logger.LogInformation("Item with title: " + products.Title + " checked for details");
            return View("~/UserInterface/Views/Products/Details.cshtml", products);
        }
        // GET: /Products/Details/1/DaysPurchased
        [HttpGet("Details/{id:int}/DaysPurchased")]
        public async Task<IActionResult> DaysPurchased(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Validation for release date missing or from the future.
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) 
            {
                return NotFound();
            }
            if (product.ReleaseDate == default)
            {
                ViewData["DaysElapsed"] = "Purchase data is missing.";
            }
            else if (product.ReleaseDate > DateTime.Now)
            {
                ViewData["DaysElapsed"] = "Not Applicable.";
            }
            else
            {
                var days = _daysPurchased.CalculateDays(product.ReleaseDate);
                ViewData["DaysElapsed"] = days;
            }
                
            ViewData["Title"] = product.Title;
            _logger.LogInformation("Item has been purchased for this long.");
            return View("~/UserInterface/Views/Products/DaysPurchased.cshtml");
        }
        // GET: Products/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View("~/UserInterface/Views/Products/Create.cshtml");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,ItemType,Price")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                _logger.LogInformation("New item created with title: " + products.Title);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/UserInterface/Views/Products/Create.cshtml", products);
        }

        // GET: Products/Edit/1
        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return View("~/UserInterface/Views/Products/Edit.cshtml", products);
        }

        // POST: Products/Edit/1
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,ItemType,Price")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                _logger.LogInformation("Item with title: " + products.Title + " edited");
                return RedirectToAction(nameof(Index));
            }
            return View("~/UserInterface/Views/Products/Edit.cshtml", products);
        }

        // GET: Products/Delete/8
        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View("~/UserInterface/Views/Products/Delete.cshtml", products);
        }

        // POST: Products/Delete/8 
        //Requires to click on the delete button.
        [HttpPost("Delete/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _logger.LogInformation("Item with title:" + products.Title + " deleted");
                _context.Products.Remove(products);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
