using GoldAggregator.Api.Dto;
using GoldAggregator.Parcer.Entities.Extensions;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Enums;

using Microsoft.EntityFrameworkCore;
using static GoldAggregator.Common.Constans;

namespace GoldAggregator.Infrastructure.Repositories.PgSql
{
    public class FiltersRepository : IFiltersRepository
    {
        private readonly ParserDbContext _context;

        public FiltersRepository(ParserDbContext context)
        {
            _context = context;
        }

        public virtual async Task<KeyLabelValuePair[]> GetFiltersAsync()
        {
            // TODO оптимизация
            var weights = await GetWeightsAsync();
            var metalTypes = await GetMetalTypesAsync();
            var maxPrices = await GetPricesAsync();
            var mintCountries = await GetMintCountriesAsync();

            var cities = new[] {
                KeyLabelValuePair.Create(key: Filters.CITY_ID, label: "Город", value: "null")
            };

            var filters = new List<KeyLabelValuePair>();

            filters.AddRange(mintCountries);
            filters.AddRange(weights);
            filters.AddRange(metalTypes);
            filters.AddRange(maxPrices);
            filters.AddRange(cities);

            return filters.ToArray();
        }

        private async Task<KeyLabelValuePair[]> GetMintCountriesAsync()
        {
            var mintCountries = await _context.CoinCatalogs
                .AsNoTracking()
                .Include(cp => cp.MintCountry)
                .GroupBy(cp => new { cp.MintCountry.Name, cp.MintCountry.TranslitName })
                .Select(g => new KeyLabelValuePair
                {
                    Key = Filters.MINT_COUNTRY_NAME,
                    Label = g.Key.Name.ToString(),
                    Value = g.Key.TranslitName.ToString()
                }).ToArrayAsync();

            //var mintCountries = await _context.MintCountries
            //    .AsNoTracking()
            //    .Select(mc => new KeyLabelValuePair
            //    {
            //        Key = Filters.MINT_COUNTRY_NAME,
            //        Label = mc.Name,
            //        Value = mc.TranslitName
            //    }).ToArrayAsync();

            return mintCountries;
        }

        private async Task<KeyLabelValuePair[]> GetWeightsAsync()
        {
            var weights = await _context.CoinCatalogs
                  .AsNoTracking()
                  .Select(cp => cp.Weight)
                  .GroupBy(weight => weight)
                  .OrderBy(weight => weight.Key)
                  .Select(g => new KeyLabelValuePair
                  {
                      Key = Filters.WEIGHT,
                      Label = g.Key.ToString(),
                      Value = g.Key.ToString()
                  })
                  .ToArrayAsync();

            return weights;
        }

        private async Task<KeyLabelValuePair[]> GetMetalTypesAsync()
        {
            var metalTypes = await _context.CoinCatalogs
                 .AsNoTracking()
                 .Select(cp => cp.MetalType)
                 .Where(mt => mt != MetalTypeEnum.Undefined)
                 .GroupBy(weight => weight)
                 .Select(g => new KeyLabelValuePair
                 {
                     Key = Filters.METAL_TYPE,
                     Label = g.Key.GetMetalName(),
                     Value = g.Key.ToString()
                 })
                 .ToArrayAsync();

            return metalTypes;
        }

        private async Task<KeyLabelValuePair[]> GetPricesAsync()
        {
            var maxPriceToBuy = await _context.CoinPrices
                .AsNoTracking()
                .GroupBy(g => new { g.CoinFromCatalogId, g.PriceToBuy })
                .Select(g => g.Key.PriceToBuy)
                .MaxAsync(priceToBuy => priceToBuy);

            var maxPriceToSell = await _context.CoinPrices
                .AsNoTracking()
                .GroupBy(g => new { g.CoinFromCatalogId, g.PriceToSell })
                .Select(g => g.Key.PriceToSell)
                .MaxAsync(priceToBuy => priceToBuy);

            var prices = new[] {
                KeyLabelValuePair.Create(Filters.MIN_PRICE_TO_SELL, "От", "0") ,
                KeyLabelValuePair.Create(Filters.MAX_PRICE_TO_SELL, "До", maxPriceToSell.ToString()),
                KeyLabelValuePair.Create(Filters.MIN_PRICE_TO_BUY, "От", "0") ,
                KeyLabelValuePair.Create(Filters.MIN_PRICE_TO_BUY, "До", maxPriceToBuy.ToString())
            };

            return prices;
        }
    }
}
