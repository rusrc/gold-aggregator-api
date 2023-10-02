using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parcer.Entities.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace GoldAggregator.UI.Admin.Mvc.Controllers
{
    // Пагинация и сортировка
    // https://docs.microsoft.com/ru-ru/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-6.0
    // 
    // На странице "Цены (монеты)" 
    // 1. CoinPrcie - ставим CoinFromCatalogId
    // 2. Создать новую запись DealerCoinMap с полями Title и Url и CoinFromCatalogId, DealerId
    //
    // Для удобсвта 
    // 1. В "цене на монеты" до бавить фильтры(По дилеру, CoinFromCatalogId по null или добавить не слинковонные)
    [Authorize]
    public class CoinPricesController : MvcBaseController
    {
        private readonly ParserDbContext _context;

        public CoinPricesController(ParserDbContext context)
        {
            _context = context;
        }

        // GET: CoinPrices
        public async Task<IActionResult> Index(int? pageNumber = 1)
        {
            var parserDbContext = _context.CoinPrices
                            .Include(c => c.CoinCatalog)
                            .Include(c => c.Dealer)
                            .Where(c => c.CoinFromCatalogId == 0 || c.CoinFromCatalogId == null);

            int pageSize = 10;
            var paginatedList = await PaginatedList<CoinPrice>.CreateAsync(parserDbContext.AsNoTracking(), pageNumber ?? 1, pageSize);
            return View(paginatedList);
        }

        // GET: CoinPrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CoinPrices == null)
            {
                return NotFound();
            }

            var coinPrice = await _context.CoinPrices
                .Include(c => c.CoinCatalog)
                .Include(c => c.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coinPrice == null)
            {
                return NotFound();
            }

            return View(coinPrice);
        }

        // GET: CoinPrices/Create
        public async Task<IActionResult> Create()
        {
            var coinFromCatalogIds = (await _context.CoinCatalogs.AsNoTracking().ToListAsync()).Select(cc => new
            {
                cc.Id,
                Name = base.GetCoinCatalogName(cc)
            }).OrderBy(x => x.Name);

            var dealersId = await _context.Dealers.OrderBy(x => x.Name).ToListAsync();
            dealersId.Insert(0, new Dealer()); // <= Empty option

            ViewData["CoinFromCatalogId"] = new SelectList(coinFromCatalogIds, "Id", "Name");
            ViewData["DealerId"] = new SelectList(dealersId, "Id", "Name");

            return View();
        }

        // POST: CoinPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Конвертация https://docs.microsoft.com/ru-ru/ef/core/modeling/value-conversions?tabs=data-annotations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoinPrice coinPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coinPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var coinFromCatalogIds = (await _context.CoinCatalogs.AsNoTracking().ToListAsync()).Select(cc => new
            {
                cc.Id,
                Name = base.GetCoinCatalogName(cc)
            }).OrderBy(x => x.Name);

            ViewData["CoinFromCatalogId"] = new SelectList(coinFromCatalogIds, "Id", "Name", coinPrice.CoinFromCatalogId);
            ViewData["DealerId"] = new SelectList(_context.Dealers.OrderBy(x => x.Name), "Id", "Name", coinPrice.DealerId);
            return View(coinPrice);
        }

        // GET: CoinPrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CoinPrices == null)
            {
                return NotFound();
            }

            var coinPrice = await _context.CoinPrices.FindAsync(id);
            if (coinPrice == null)
            {
                return NotFound();
            }

            var coinFromCatalogIds = (await _context.CoinCatalogs.AsNoTracking().ToListAsync()).Select(cc => new
            {
                cc.Id,
                Name = base.GetCoinCatalogName(cc)
            }).OrderBy(x => x.Name);

            ViewData["CoinFromCatalogId"] = new SelectList(coinFromCatalogIds, "Id", "Name", coinPrice.CoinFromCatalogId);
            ViewData["DealerId"] = new SelectList(_context.Dealers.OrderBy(x => x.Name), "Id", "Name", coinPrice.DealerId);
            return View(coinPrice);
        }

        // POST: CoinPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Url,PriceToBuy,PriceToBuyPerGram,PriceToSell,PriceToSellPerGram,PriceSpecial,PriceSpecialPerGram,PriceSpecialDetails,ParseDate,Error,DealerId,CoinFromCatalogId")] CoinPrice coinPrice)
        {
            if (id != coinPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dealerCoinMap = await _context.DillerCoinMaps.SingleOrDefaultAsync(dcm =>
                         dcm.DealerId == coinPrice.DealerId &&
                         dcm.MatchedTitle == coinPrice.Title &&
                         dcm.Url == coinPrice.Url &&
                         dcm.CoinFromCatalogId == (int)coinPrice.CoinFromCatalogId);

                    if (dealerCoinMap == null)
                    {
                        dealerCoinMap = new DealerCoinMap
                        {
                            DealerId = coinPrice.DealerId,
                            MatchedTitle = coinPrice.Title,
                            Url = coinPrice.Url,
                            CoinFromCatalogId = (int)coinPrice.CoinFromCatalogId
                        };

                        await _context.DillerCoinMaps.AddAsync(dealerCoinMap);
                    }

                    _context.Update(coinPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoinPriceExists(coinPrice.Id))
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

            var coinFromCatalogIds = (await _context.CoinCatalogs.AsNoTracking().ToListAsync()).Select(cc => new
            {
                cc.Id,
                Name = base.GetCoinCatalogName(cc)
            }).OrderBy(x => x.Name);

            ViewData["CoinFromCatalogId"] = new SelectList(coinFromCatalogIds, "Id", "Name", coinPrice.CoinFromCatalogId);
            ViewData["DealerId"] = new SelectList(_context.Dealers.OrderBy(x => x.Name), "Id", "Name", coinPrice.DealerId);
            return View(coinPrice);
        }

        // GET: CoinPrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CoinPrices == null)
            {
                return NotFound();
            }

            var coinPrice = await _context.CoinPrices
                .Include(c => c.CoinCatalog)
                .Include(c => c.Dealer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coinPrice == null)
            {
                return NotFound();
            }

            return View(coinPrice);
        }

        // POST: CoinPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CoinPrices == null)
            {
                return Problem("Entity set 'ParserDbContext.CoinPrices'  is null.");
            }
            var coinPrice = await _context.CoinPrices.FindAsync(id);
            if (coinPrice != null)
            {
                _context.CoinPrices.Remove(coinPrice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoinPriceExists(int id)
        {
            return (_context.CoinPrices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
