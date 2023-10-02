using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using Microsoft.AspNetCore.Authorization;

namespace GoldAggregator.UI.Admin.Mvc.Controllers
{
    [Authorize]
    public class DealersController : Controller
    {
        private readonly ParserDbContext _context;

        public DealersController(ParserDbContext context)
        {
            _context = context;
        }

        // GET: Dealers
        public async Task<IActionResult> Index()
        {
              return _context.Dealers != null ? 
                          View(await _context.Dealers.ToListAsync()) :
                          Problem("Entity set 'ParserDbContext.Dealers'  is null.");
        }

        // GET: Dealers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dealers == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // GET: Dealers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dealers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ProviderName,BaseUrl,HasDelivery,DealerType")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dealer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dealer);
        }

        // GET: Dealers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dealers == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }
            return View(dealer);
        }

        // POST: Dealers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ProviderName,BaseUrl,HasDelivery,DealerType")] Dealer dealer)
        {
            if (id != dealer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dealer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealerExists(dealer.Id))
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
            return View(dealer);
        }

        // GET: Dealers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dealers == null)
            {
                return NotFound();
            }

            var dealer = await _context.Dealers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealer == null)
            {
                return NotFound();
            }

            return View(dealer);
        }

        // POST: Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dealers == null)
            {
                return Problem("Entity set 'ParserDbContext.Dealers'  is null.");
            }
            var dealer = await _context.Dealers.FindAsync(id);
            if (dealer != null)
            {
                _context.Dealers.Remove(dealer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealerExists(int id)
        {
          return (_context.Dealers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
