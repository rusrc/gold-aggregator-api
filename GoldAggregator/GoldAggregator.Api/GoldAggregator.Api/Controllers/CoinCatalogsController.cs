using GoldAggregator.Parser.DbContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinCatalogsController : ControllerBase
    {
        private readonly ParserDbContext _context;
        private readonly ILogger<CoinsPriceController> _logger;

        public CoinCatalogsController(
            ParserDbContext context,
            ILogger<CoinsPriceController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Монета из каталога по Id
        /// </summary>
        /// <param name="coinCatalogId">Id каталога</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CoinFromCatalog")]
        public async Task<IActionResult> GetCoinFromCatalog(int coinCatalogId)
        {
            var catalog = await _context.CoinCatalogs
                .Include(cc => cc.MintCountry)
                .SingleOrDefaultAsync(c => c.Id == coinCatalogId);

            if (catalog == null) return NotFound();

            return Ok(catalog);
        }
    }
}
