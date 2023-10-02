using GoldAggregator.Api.Dto;
using GoldAggregator.Api.Dto.Enums;
using GoldAggregator.Parcer.Entities.Extensions;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Extension;

using GoldAggregator.Parser.Logger;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoldAggregator.Infrastructure.Repositories.PgSql
{
    public class CoinPriceRepository : ICoinPriceRepository
    {
        private readonly ILogger<CoinPriceRepository> _logger;
        private readonly ParserDbContext _context;

        public CoinPriceRepository(ILogger<CoinPriceRepository> logger, ParserDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public virtual async Task<PagedListDto<CoinCatalogDto>> GetPagedCoinPricesAsync(
            KeyValuePair<string, string>[] filters,
            int? page = 1,
            int? pageSize = 10,
            SortCatalogCoins sortBy = SortCatalogCoins.None,
            SortDirection direction = SortDirection.Asc)
        {
            using var t = new TimePointer<CoinPriceRepository>(nameof(GetPagedCoinPricesAsync), _logger);

            var cityId = filters.GetCityId();
            var weights = filters.GetWeights();
            var metals = filters.GetMetalTypes();
            var minPriceToSell = filters.GetMinPriceToSell();
            var maxPriceToSell = filters.GetMaxPriceToSell();
            var minPriceToBuy = filters.GetMinPriceToBuy();
            var maxPriceToBuy = filters.GetMaxPriceToBuy();
            var mintCountryNames = filters.GetMintCountryNames();


            maxPriceToSell = maxPriceToSell == 0 ? int.MaxValue : maxPriceToSell;
            maxPriceToBuy = maxPriceToBuy == 0 ? int.MaxValue : maxPriceToBuy;

            var query = await QueryCoinCatalogWithPricesAsync(weights, metals, minPriceToSell, maxPriceToSell, minPriceToBuy, maxPriceToBuy, cityId, mintCountryNames);
            var totalItems = await query.CountAsync();

            switch (sortBy)
            {
                case SortCatalogCoins.BuyPrice:
                    query = direction == SortDirection.Asc ? query.OrderBy(i => i.MaxPriceToBuy) : query.OrderByDescending(i => i.MaxPriceToBuy); break;
                case SortCatalogCoins.SellPrice:
                    query = direction == SortDirection.Asc ? query.OrderBy(i => i.MinPriceToSell) : query.OrderByDescending(i => i.MinPriceToSell); break;
                case SortCatalogCoins.BuyPricePerGram:
                    query = direction == SortDirection.Asc ? query.OrderBy(i => i.MaxPriceToBuyPerGram) : query.OrderByDescending(i => i.MaxPriceToBuyPerGram); break;
                case SortCatalogCoins.SellPricePerGram:
                    query = direction == SortDirection.Asc ? query.OrderBy(i => i.MinPriceToSellPerGram) : query.OrderByDescending(i => i.MinPriceToSellPerGram); break;
                default: break;
            }

            var pagedList = await query.ToPagedListAsync((int)pageSize, (int)page, totalItems); t.Add("[NEXT] get grouped coins successeded");

            // TODO remove this workaround later
            pagedList.Items.ToList().ForEach(item => item.MetalTypeName = ((MetalTypeEnum)item.MetalType).GetMetalName());


            var result = new PagedListDto<CoinCatalogDto>
            {
                Items = pagedList.Items, // mappedItems,
                PageSize = pagedList.PageSize,
                FirstItemOnPage = pagedList.FirstItemOnPage,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                IsFirstPage = pagedList.IsFirstPage,
                IsLastPage = pagedList.IsLastPage,
                LastItemOnPage = pagedList.LastItemOnPage,
                PageCount = pagedList.PageCount,
                PageNumber = pagedList.PageNumber,
                TotalItemCount = pagedList.TotalItemCount,
            };

            return result;
        }

        // TODO maybe rename or remove as used for SEO cache only
        /// <summary>
        /// For SEO only
        /// As used for SEO cache only
        /// </summary>
        public async Task<CoinCatalogDto?> GetCoinCatalogWithPricesAsync(int coinCatalogId, int cityId = 0)
        {
            using var p = new TimePointer<CoinPriceRepository>($"{nameof(GetCoinCatalogWithPricesAsync)}", _logger);

            // TODO вынести наружу и добавить кеш на 1 час не меньше см. DealerRepository
            var filteredDealersByCityId = _context.Dealers.Include(d => d.Offices).Where(d => d.HasDelivery == true || d.Offices.Any(o => o.CityId == cityId));
            var dealersId = await filteredDealersByCityId.Select(d => d.Id).ToArrayAsync();

            var hasCity = cityId != 0;

            var coinCatalog = await _context.CoinCatalogs
                    .Where(cc => cc.Id == coinCatalogId)
                    .Include(c => c.CoinPrices)
                    .Include(c => c.MintCountry)
                    .GroupBy(cc => new
                    {
                        cc.Id,
                        cc.Name,
                        cc.TranslitName,
                        cc.ImageName,
                        cc.MintCountryId,
                        MintCountryName = cc.MintCountry.Name,
                        MintCountryTranslitName = cc.MintCountry.TranslitName,
                        cc.SeoUrl,
                        cc.Weight,
                        cc.MetalType,

                        MinPriceToBuy = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToBuy != null).Min(p => p.PriceToBuy) :
                            cc.CoinPrices.Where(c => c.PriceToBuy != null).Min(p => p.PriceToBuy),
                        MaxPriceToBuy = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToBuy != null).Max(p => p.PriceToBuy) :
                            cc.CoinPrices.Where(c => c.PriceToBuy != null).Max(p => p.PriceToBuy),
                        MinPriceToSell = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToSell != null).Min(p => p.PriceToSell) :
                            cc.CoinPrices.Where(c => c.PriceToSell != null).Min(p => p.PriceToSell),
                        MaxPriceToSell = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToSell != null).Max(p => p.PriceToSell) :
                            cc.CoinPrices.Where(c => c.PriceToSell != null).Max(p => p.PriceToSell),
                        MaxPriceToBuyPerGram = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToBuyPerGram != null).Max(p => p.PriceToBuyPerGram) :
                            cc.CoinPrices.Where(c => c.PriceToBuyPerGram != null).Max(p => p.PriceToBuyPerGram),
                        MinPriceToSellPerGram = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToSellPerGram != null).Min(p => p.PriceToSellPerGram) :
                            cc.CoinPrices.Where(c => c.PriceToSellPerGram != null).Min(p => p.PriceToSellPerGram),

                    }).Select(g => new CoinCatalogDto
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                        ImageName = g.Key.ImageName,
                        MintCountryId = g.Key.MintCountryId,
                        MintCountryName = g.Key.MintCountryName,
                        MintCountryTranslitName = g.Key.MintCountryTranslitName,
                        SeoUrl = g.Key.SeoUrl + "_" + g.Key.Weight + "_" + g.Key.Id,
                        TranslitName = g.Key.TranslitName,
                        Weight = g.Key.Weight,
                        MetalType = (int)g.Key.MetalType,
                        MinPriceToBuy = g.Key.MinPriceToBuy,
                        MaxPriceToBuy = g.Key.MaxPriceToBuy,
                        MinPriceToSell = g.Key.MinPriceToSell, // Покупка От.
                        MaxPriceToSell = g.Key.MaxPriceToSell,
                        MaxPriceToBuyPerGram = g.Key.MaxPriceToBuyPerGram,
                        MinPriceToSellPerGram = g.Key.MinPriceToSellPerGram, // Покупка От.                                
                    })
                    .FirstOrDefaultAsync();

            return coinCatalog;
        }

        public async Task<IQueryable<CoinCatalogDto>> QueryCoinCatalogWithPricesAsync(
            IEnumerable<decimal>? weights,
            IEnumerable<int>? metals,
            decimal minPriceToSell,
            decimal maxPriceToSell,
            decimal minPriceToBuy,
            decimal maxPriceToBuy,
            int cityId,
            IEnumerable<string> mintCountryTranslitNames)
        {
            // TODO вынести наружу и добавить кеш на 1 час не меньше см. DealerRepository
            var filteredDealersByCityId = _context.Dealers.Include(d => d.Offices).Where(d => d.HasDelivery == true || d.Offices.Any(o => o.CityId == cityId));
            var dealersId = await filteredDealersByCityId.Select(d => d.Id).ToArrayAsync();

            bool hasCity = cityId != 0;

            // Custom db funcion https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/how-to-call-custom-database-functions?redirectedfrom=MSDN
            var coinCatalogsQuery = _context.CoinCatalogs
                    .Include(c => c.CoinPrices)
                    .Include(c => c.MintCountry)
                    .GroupBy(cc => new
                    {
                        cc.Id,
                        cc.Name,
                        cc.TranslitName,
                        cc.ImageName,
                        cc.MintCountryId,
                        MintCountryName = cc.MintCountry.Name,
                        MintCountryTranslitName = cc.MintCountry.TranslitName,
                        cc.SeoUrl,
                        cc.Weight,
                        cc.MetalType,

                        MinPriceToBuy = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToBuy != null).Min(p => p.PriceToBuy) :
                            cc.CoinPrices.Where(c => c.PriceToBuy != null).Min(p => p.PriceToBuy),
                        MaxPriceToBuy = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToBuy != null).Max(p => p.PriceToBuy) :
                            cc.CoinPrices.Where(c => c.PriceToBuy != null).Max(p => p.PriceToBuy),
                        MinPriceToSell = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToSell != null).Min(p => p.PriceToSell) :
                            cc.CoinPrices.Where(c => c.PriceToSell != null).Min(p => p.PriceToSell),
                        MaxPriceToSell = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToSell != null).Max(p => p.PriceToSell) :
                            cc.CoinPrices.Where(c => c.PriceToSell != null).Max(p => p.PriceToSell),
                        MaxPriceToBuyPerGram = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToBuyPerGram != null).Max(p => p.PriceToBuyPerGram) :
                            cc.CoinPrices.Where(c => c.PriceToBuyPerGram != null).Max(p => p.PriceToBuyPerGram),
                        MinPriceToSellPerGram = hasCity ?
                            cc.CoinPrices.Where(c => dealersId.Contains(c.DealerId) && c.PriceToSellPerGram != null).Min(p => p.PriceToSellPerGram) :
                            cc.CoinPrices.Where(c => c.PriceToSellPerGram != null).Min(p => p.PriceToSellPerGram),

                    })
                    .Select(g => new CoinCatalogDto
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name,
                        ImageName = g.Key.ImageName,
                        MintCountryId = g.Key.MintCountryId,
                        MintCountryName = g.Key.MintCountryName,
                        MintCountryTranslitName = g.Key.MintCountryTranslitName,
                        SeoUrl = g.Key.SeoUrl + "_" + g.Key.Weight + "_" + g.Key.Id,
                        TranslitName = g.Key.TranslitName,
                        Weight = g.Key.Weight,
                        MetalType = (int)g.Key.MetalType,
                        MinPriceToBuy = g.Key.MinPriceToBuy,
                        MaxPriceToBuy = g.Key.MaxPriceToBuy,
                        MinPriceToSell = g.Key.MinPriceToSell, // Покупка От.
                        MaxPriceToSell = g.Key.MaxPriceToSell,
                        MaxPriceToBuyPerGram = g.Key.MaxPriceToBuyPerGram,
                        MinPriceToSellPerGram = g.Key.MinPriceToSellPerGram, // Покупка От.                               
                    })

                    //Пример, разброс цен продажи монет от 1100 до 1500р,  соответсвенно если ставят фильтр Цены продажи от 1000 до 1300
                    //то             1000        <=    1500                                         &&   1100             <=        1300
                    .Where(cc => (minPriceToSell <= cc.MaxPriceToSell || cc.MaxPriceToSell == null) && (cc.MinPriceToSell <= maxPriceToSell || cc.MinPriceToSell == null))
                    //Пример, разброс цен покупки монет от 1100 до 1500р,  соответсвенно если ставят фильтр Цены покупки от 1000 до 1300
                    //то            1000        <=    1500                                       &&   1100            <=        1300
                    .Where(cc => (minPriceToBuy <= cc.MaxPriceToBuy || cc.MaxPriceToBuy == null) && (cc.MinPriceToBuy <= maxPriceToBuy || cc.MinPriceToBuy == null))
                    //Выводим только те предложения, где есть или продажа или покупка или оба вариант
                    .Where(cc => cc.MinPriceToSell != null || cc.MaxPriceToSell != null || cc.MinPriceToBuy != null || cc.MaxPriceToBuy != null)
                    .AsQueryable();

            // Filter by weights
            if (!weights.IsCollectionEmpty()) 
                coinCatalogsQuery = coinCatalogsQuery.Where(c => weights.Contains(c.Weight));

            // Filter by MetalTypes
            if (!metals.IsCollectionEmpty()) 
                coinCatalogsQuery = coinCatalogsQuery.Where(c => metals.Contains(c.MetalType));

            // Filter by mintCountryNames
            if (!mintCountryTranslitNames.IsCollectionEmpty()) 
                coinCatalogsQuery = coinCatalogsQuery.Where(c => mintCountryTranslitNames.Contains(c.MintCountryTranslitName));

            return coinCatalogsQuery;
        }

        /// <summary>
        /// { CoinCatalogId, ParseDate}
        /// </summary>
        /// <param name="coinCatalogIds"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, DateTime>> GetLastUpdatedCoinCatalogsAsync(int[] coinCatalogIds)
        {
            var dictionary = await _context.CoinPrices
                .Where(c => c.CoinFromCatalogId.HasValue)
                .Where(c => coinCatalogIds.Contains((int)c.CoinFromCatalogId))
                .GroupBy(x => x.CoinFromCatalogId, y => y.ParseDate,
                    (coinCatalogId, ParseDates) => new
                    {
                        CoinCatalogId = coinCatalogId.GetValueOrDefault(),
                        ParseDate = ParseDates.Max()
                    })
                .ToDictionaryAsync(obj => obj.CoinCatalogId, obj => obj.ParseDate);


            return dictionary;
        }
    }
}
