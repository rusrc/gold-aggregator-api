using AutoMapper;

using GoldAggregator.Api.Dto.Enums;
using GoldAggregator.Infrastructure.Repositories;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Logger;
using GoldAggregator.Parser.Services;
using GoldAggregator.Parser.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoinsPriceController : ControllerBase
    {
        private readonly ParserDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CoinsPriceController> _logger;
        private readonly ICoinPriceRepository _coinPriceRepository;
        private readonly ISeoService _seoService;

        public CoinsPriceController(
            ParserDbContext context,
            IMapper mapper,
            ILogger<CoinsPriceController> logger,
            ICoinPriceRepository coinPriceRepository,
            ISeoService seoService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _coinPriceRepository = coinPriceRepository ?? throw new ArgumentNullException(nameof(coinPriceRepository));
            _seoService = seoService ?? throw new ArgumentNullException(nameof(seoService));
        }

        /// <summary>
        /// Выдаем Список монет по фильтру + цены 
        /// Выдаем по фильтру (город, тип металла, вес и т.д)
        /// </summary>
        /// <param name="filters">Filters {"FilterName": "FilterValue"}
        /// Пример запроса
        /// [
        ///  {
        ///    "key": "Price",
        ///     "value": "0"
        ///  },
        ///  {
        ///    "key": "Price",
        ///    "value": "100000"
        ///  },
        ///  {
        ///    "key": "Weight",
        ///    "value": "15,55"
        ///  },
        ///  {
        ///    "key": "Weight",
        ///    "value": "7,78"
        ///  },
        ///  {
        ///    "key": "MetalType",
        ///    "value": "Gold"
        ///  },
        ///  {
        ///    "key": "CityId",
        ///    "value": "1"
        ///  }
        /// ]
        /// </param>
        /// <param name="page">Номер страницы, по умолчанию 1</param>
        /// <param name="pageSize">Количество позиций на страницу, по умолчанию 10</param>
        /// <param name="sortBy">sortBy</param>
        /// <param name="sortDirection">asc / desc</param>
        [HttpPost]
        [Route("CoinCatalogWithPrices")]
        public async Task<IActionResult> GetCoinPriceByCoinCatalogs(
            [FromBody] KeyValuePair<string, string>[] filters,
            int? page = 1,
            int? pageSize = 10,
            SortCatalogCoins sortBy = SortCatalogCoins.None,
            SortDirection sortDirection = SortDirection.Asc)
        {
            // using var t = new TimePointer<CoinsPriceController>(nameof(GetCoinPriceByCoinCatalogs), _logger);
            var pagedList = await _coinPriceRepository.GetPagedCoinPricesAsync(filters, page, pageSize, sortBy, sortDirection);

            //pagedList.Seo = seo;

            return Ok(pagedList);
        }

        /// <summary>
        /// CoinPrice - Все цены от дилеров по конкретной монете в указанном городе
        /// </summary>
        /// <param name="CoinCatalogId">Id монеты в нашем каталоге</param>
        /// <param name="cityId">Id города</param>
        [HttpGet]
        [Route("CoinPrices")]
        public async Task<IActionResult> GetCoinPrices(long CoinCatalogId, long? cityId)
        {
            using var timer = new TimePointer<CoinsPriceController>(nameof(GetCoinPrices), _logger);
            
            var coinPrices = await _context.CoinPrices
                .Include(cp => cp.Dealer)
                    .ThenInclude(cp => cp.Offices)
                .Where(c =>
                    (c.Dealer.HasDelivery == true || c.Dealer.Offices.Any(c => c.CityId == cityId)) && c.CoinFromCatalogId == CoinCatalogId)
                .ToListAsync();

            #region variant2
            //Это вариант 2 для фильтрации городов. Протестить что будет быстрее
            ////ToDo чуть позже добавить кеш на 1 час не меньше
            //var filteredDealersByCityId = _context.Dealers.Include(d => d.Offices)
            //    .Where(d => d.HasDelivery == true || d.Offices.Any(o => o.CityId == cityId));
            //var filteredDealersId = await filteredDealersByCityId.Select(d => d.Id).ToArrayAsync();

            //var coinPrices = await _context.CoinPrices               
            //   .Where(c => 
            //    c.CoinFromCatalogId == CoinCatalogId &&
            //    filteredDealersId.Contains(c.DealerId)                
            //    ).ToListAsync();
            #endregion

            timer.Add("[NEXT] get by cityId coin prices successeded");

            return Ok(coinPrices);

        }

        /// <summary>
        /// Сохраняем клик перехода на страничку с продажей монеты у дилера
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="cityId">Id города</param>
        /// <returns></returns>
        [HttpPut]
        [Route("CoinPriceClick")]
        public async Task<IActionResult> PutCoinPriceClick(string url, int? cityId, bool? isClickBySellPrice)
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var now = DateTime.Now;
            var hoursMinus1 = DateTime.Now.AddHours(-1);
            var coinPriceHistory = await _context.CoinsPriceHistory.Where(
                     c => c.Url == url &&
                     c.ParseDate <= now &&
                     c.ParseDate >= hoursMinus1)
                    .OrderByDescending(c => c.ParseDate).FirstOrDefaultAsync();

            //Что бы не потерять клик, сохраняем на более старую цену. Но за то точно на ту монету и на того дилера.
            if (coinPriceHistory == null)
            {
                coinPriceHistory = await _context.CoinsPriceHistory.Where(
                        c => c.Url == url)
                       .OrderByDescending(c => c.ParseDate).FirstOrDefaultAsync();
            }

            if (coinPriceHistory != null)
            {
                await _context.AddAsync(new CoinPriceClick()
                {
                    CoinHistoryId = coinPriceHistory.Id,
                    Ip = remoteIpAddress,
                    RedirectDate = DateTime.Now,
                    CityId = cityId,
                    IsClickBySellPrice = isClickBySellPrice
                });
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
