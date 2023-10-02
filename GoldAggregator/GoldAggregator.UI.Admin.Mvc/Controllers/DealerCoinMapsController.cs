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
    // Пагинаци и сортировка
    // https://docs.microsoft.com/ru-ru/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-6.0
    [Authorize]
    public class DealerCoinMapsController : MvcBaseController
    {
        private readonly ParserDbContext _context;

        public DealerCoinMapsController(ParserDbContext context)
        {
            _context = context;
        }

        // GET: DealerCoinMaps
        public async Task<IActionResult> Index()
        {
            var parserDbContext = _context.DillerCoinMaps.Include(d => d.CoinCatalog).Include(d => d.Dealer);
            return View(await parserDbContext.ToListAsync());
        }

        // GET: DealerCoinMaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DillerCoinMaps == null)
            {
                return NotFound();
            }

            var dealerCoinMap = await _context.DillerCoinMaps
                .Include(d => d.CoinCatalog)
                .Include(d => d.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealerCoinMap == null)
            {
                return NotFound();
            }

            return View(dealerCoinMap);
        }

        // GET: DealerCoinMaps/Create
        public async Task<IActionResult> Create()
        {
            var coinCatalogs = await _context.CoinCatalogs.ToListAsync();
            var selectItems = coinCatalogs.Select(cc => new { 
                cc.Id,
                Name = base.GetCoinCatalogName(cc),
            });

            ViewData["CoinFromCatalogId"] = new SelectList(selectItems, "Id", "Name");
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name");
            return View();
        }

        // POST: DealerCoinMaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DealerCoinMap dealerCoinMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dealerCoinMap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var coinCatalogs = await _context.CoinCatalogs.ToListAsync();
            var selectItems = coinCatalogs.Select(cc => new {
                cc.Id,
                Name = base.GetCoinCatalogName(cc),
            });

            ViewData["CoinFromCatalogId"] = new SelectList(selectItems, "Id", "Name", dealerCoinMap.CoinFromCatalogId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", dealerCoinMap.DealerId);
            return View(dealerCoinMap);
        }

        // GET: DealerCoinMaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DillerCoinMaps == null)
            {
                return NotFound();
            }

            var dealerCoinMap = await _context.DillerCoinMaps.FindAsync(id);
            if (dealerCoinMap == null)
            {
                return NotFound();
            }

            var coinCatalogs = await _context.CoinCatalogs.ToListAsync();
            var selectItems = coinCatalogs.Select(cc => new {
                cc.Id,
                Name = base.GetCoinCatalogName(cc),
            }).OrderBy(c=>c.Name);

            ViewData["CoinFromCatalogId"] = new SelectList(selectItems, "Id", "Name");
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", dealerCoinMap.DealerId);
            return View(dealerCoinMap);
        }

        // POST: DealerCoinMaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DealerCoinMap dealerCoinMap)
        {
            if (id != dealerCoinMap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dealerCoinMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealerCoinMapExists(dealerCoinMap.Id))
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

            var coinCatalogs = await _context.CoinCatalogs.ToListAsync();
            var selectItems = coinCatalogs.Select(cc => new {
                cc.Id,
                Name = base.GetCoinCatalogName(cc),
            });

            ViewData["CoinFromCatalogId"] = new SelectList(selectItems, "Id", "Name");
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", dealerCoinMap.DealerId);
            return View(dealerCoinMap);
        }

        // GET: DealerCoinMaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DillerCoinMaps == null)
            {
                return NotFound();
            }

            var dealerCoinMap = await _context.DillerCoinMaps
                .Include(d => d.CoinCatalog)
                .Include(d => d.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealerCoinMap == null)
            {
                return NotFound();
            }

            return View(dealerCoinMap);
        }

        // POST: DealerCoinMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DillerCoinMaps == null)
            {
                return Problem("Entity set 'ParserDbContext.DillerCoinMaps'  is null.");
            }
            var dealerCoinMap = await _context.DillerCoinMaps.FindAsync(id);
            if (dealerCoinMap != null)
            {
                _context.DillerCoinMaps.Remove(dealerCoinMap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealerCoinMapExists(int id)
        {
          return (_context.DillerCoinMaps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
