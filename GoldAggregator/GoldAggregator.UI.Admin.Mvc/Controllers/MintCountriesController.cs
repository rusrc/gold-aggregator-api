using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using GoldAggregator.Parser.Services;

namespace GoldAggregator.UI.Admin.Mvc.Controllers
{
    [Authorize]
    public class MintCountriesController : Controller
    {
        private readonly ParserDbContext _context;

        public MintCountriesController(ParserDbContext context)
        {
            _context = context;
        }

        // GET: MintCountries
        public async Task<IActionResult> Index()
        {
              return _context.MintCountries != null ? 
                          View(await _context.MintCountries.ToListAsync()) :
                          Problem("Entity set 'ParserDbContext.MintCountries'  is null.");
        }

        // GET: MintCountries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MintCountries == null)
            {
                return NotFound();
            }

            var mintCountry = await _context.MintCountries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mintCountry == null)
            {
                return NotFound();
            }

            return View(mintCountry);
        }

        // GET: MintCountries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MintCountries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TranslitName")] MintCountry mintCountry)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(mintCountry.TranslitName))
                {
                    mintCountry.TranslitName = Transliteration.Generate(mintCountry.Name);
                }

                _context.Add(mintCountry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mintCountry);
        }

        // GET: MintCountries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MintCountries == null)
            {
                return NotFound();
            }

            var mintCountry = await _context.MintCountries.FindAsync(id);
            if (mintCountry == null)
            {
                return NotFound();
            }
            return View(mintCountry);
        }

        // POST: MintCountries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TranslitName")] MintCountry mintCountry)
        {
            if (id != mintCountry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mintCountry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MintCountryExists(mintCountry.Id))
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
            return View(mintCountry);
        }

        // GET: MintCountries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MintCountries == null)
            {
                return NotFound();
            }

            var mintCountry = await _context.MintCountries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mintCountry == null)
            {
                return NotFound();
            }

            return View(mintCountry);
        }

        // POST: MintCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MintCountries == null)
            {
                return Problem("Entity set 'ParserDbContext.MintCountries'  is null.");
            }
            var mintCountry = await _context.MintCountries.FindAsync(id);
            if (mintCountry != null)
            {
                _context.MintCountries.Remove(mintCountry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MintCountryExists(int id)
        {
          return (_context.MintCountries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
