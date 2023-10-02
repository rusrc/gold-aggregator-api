using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;

namespace GoldAggregator.UI.Admin.Mvc.Controllers
{
    public class DealerOfficesController : Controller
    {
        private readonly ParserDbContext _context;

        public DealerOfficesController(ParserDbContext context)
        {
            _context = context;
        }

        // GET: DealerOffices
        public async Task<IActionResult> Index()
        {
            var parserDbContext = _context.DealerOffices
                .Include(d => d.City)
                .Include(d => d.Dealer);

            return View(await parserDbContext.ToListAsync());
        }

        // GET: DealerOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DealerOffices == null)
            {
                return NotFound();
            }

            var dealerOffice = await _context.DealerOffices
                .Include(d => d.City)
                .Include(d => d.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealerOffice == null)
            {
                return NotFound();
            }

            return View(dealerOffice);
        }

        // GET: DealerOffices/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name");
            return View();
        }

        // POST: DealerOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CityId,DealerId")] DealerOffice dealerOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dealerOffice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", dealerOffice.CityId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", dealerOffice.DealerId);
            return View(dealerOffice);
        }

        // GET: DealerOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DealerOffices == null)
            {
                return NotFound();
            }

            var dealerOffice = await _context.DealerOffices.FindAsync(id);
            if (dealerOffice == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", dealerOffice.CityId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", dealerOffice.DealerId);
            return View(dealerOffice);
        }

        // POST: DealerOffices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CityId,DealerId")] DealerOffice dealerOffice)
        {
            if (id != dealerOffice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dealerOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DealerOfficeExists(dealerOffice.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", dealerOffice.CityId);
            ViewData["DealerId"] = new SelectList(_context.Dealers, "Id", "Name", dealerOffice.DealerId);
            return View(dealerOffice);
        }

        // GET: DealerOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DealerOffices == null)
            {
                return NotFound();
            }

            var dealerOffice = await _context.DealerOffices
                .Include(d => d.City)
                .Include(d => d.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dealerOffice == null)
            {
                return NotFound();
            }

            return View(dealerOffice);
        }

        // POST: DealerOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DealerOffices == null)
            {
                return Problem("Entity set 'ParserDbContext.DealerOffices'  is null.");
            }
            var dealerOffice = await _context.DealerOffices.FindAsync(id);
            if (dealerOffice != null)
            {
                _context.DealerOffices.Remove(dealerOffice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DealerOfficeExists(int id)
        {
          return (_context.DealerOffices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
