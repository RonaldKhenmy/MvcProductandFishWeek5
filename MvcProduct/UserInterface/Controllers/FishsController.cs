using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcProduct.Infrastructure.Data;
using MvcProduct.UserInterface.Models;

namespace MvcProduct.UserInterface.Controllers
{
    public class FishsController : Controller
    {
        private readonly MvcFishContext _context;
        private readonly ILogger<FishsController> _logger;
        public FishsController(MvcFishContext context, ILogger<FishsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Fishs
        public async Task<IActionResult> Index()
        {
            return View("~/UserInterface/Views/Fishs/Index.cshtml", await _context.Fishs.ToListAsync());
        }

        // GET: Fishs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishs = await _context.Fishs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishs == null)
            {
                return NotFound();
            }

            return View("~/UserInterface/Views/Fishs/Details.cshtml", fishs);
        } 


        // GET: Fishs/Create
        public IActionResult Create()
        {
            return View("~/UserInterface/Views/Fishs/Create.cshtml");
        }

        // POST: Fishs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fish,CaughtDate,WaterType,Age,Price")] Fishs fishs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fishs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/UserInterface/Views/Fishs/Create.cshtml", fishs);
        }

        // GET: Fishs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishs = await _context.Fishs.FindAsync(id);
            if (fishs == null)
            {
                return NotFound();
            }
            return View("~/UserInterface/Views/Fishs/Edit.cshtml", fishs);
        }

        // POST: Fishs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fish,CaughtDate,WaterType,Age,Price")] Fishs fishs)
        {
            if (id != fishs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fishs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishsExists(fishs.Id))
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
            return View("~/UserInterface/Views/Fishs/Edit.cshtml", fishs);
        }

        // GET: Fishs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishs = await _context.Fishs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fishs == null)
            {
                return NotFound();
            }

            return View("~/UserInterface/Views/Fishs/Delete.cshtml", fishs);
        }

        // POST: Fishs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fishs = await _context.Fishs.FindAsync(id);
            if (fishs != null)
            {
                _context.Fishs.Remove(fishs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FishsExists(int id)
        {
            return _context.Fishs.Any(e => e.Id == id);
        }

        //GET Fishs/MAInput
        public IActionResult MAInput(MessageActionViewModel model)
        {
            return View("~/UserInterface/Views/Fishs/CreateMA.cshtml");
        }

        //Post Redirect
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MASubmit(MessageActionViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["RedirectMessage"] = "Success, you did it!";
                return RedirectToAction("DisplayMA", new { messageAction = model.MessageAction });
            }
            return View("~/UserInterface/Views/Fishs/CreateMA.cshtml", model);
        } 

        //GET: Fishs/DisplayMA
        public IActionResult DisplayMA(string messageAction)
        {
            var model = new MessageActionViewModel
            {
                MessageAction = messageAction
            };
            if (TempData["RedirectMessage"] is string redirectMessage && redirectMessage == "Success, you did it!")
            {
                _logger.LogInformation("{Message}", redirectMessage);
                ViewBag.RedirectMessage = redirectMessage;
            }
            return View("~/UserInterface/Views/Fishs/DisplayMA.cshtml", model);
        }

        //GET: Fishs/EditMA
        public IActionResult EditMA(string messageAction)
        {
            var model = new MessageActionViewModel
            {
                MessageAction = messageAction
            };
            return View("~/UserInterface/Views/Fishs/EditMA.cshtml", model);
        }

        //POST: Fishs/EditMA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMASubmit(MessageActionViewModel model)
        {

            if (ModelState.IsValid)
            {
           
                return RedirectToAction("DisplayMA", new {messageAction = model.MessageAction});
            }
            return View("~/UserInterface/Views/Fishs/EditMA.cshtml", model);
        }
    }   

}
