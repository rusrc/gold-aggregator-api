using GoldAggregator.Parser.Entities.Enums;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static GoldAggregator.Common.Constans;

namespace GoldAggregator.Parser.Extension
{
    public static class FilterExtension
    {
        public static IEnumerable<decimal> GetWeights(this KeyValuePair<string, string>[] filters) => filters
                .Where(f => f.Key == Filters.WEIGHT)
                .Select(f => decimal.Parse(f.Value, CultureInfo.InvariantCulture));
       
        public static IEnumerable<int> GetMetalTypes(this KeyValuePair<string, string>[] filters) => filters
                .Where(f => f.Key == Filters.METAL_TYPE)
                .Select(f => (int)Enum.Parse<MetalTypeEnum>(f.Value));
        
        public static int GetCityId(this KeyValuePair<string, string>[] filters) => filters
                .Where(f => f.Key == Filters.CITY_ID)
                .Select(f => int.Parse(f.Value, CultureInfo.InvariantCulture))
                .SingleOrDefault();
        
        public static decimal GetMinPriceToSell(this KeyValuePair<string, string>[] filters)
            => GetDecimal(filters, Filters.MIN_PRICE_TO_SELL);
        
        public static decimal GetMaxPriceToSell(this KeyValuePair<string, string>[] filters)
            => GetDecimal(filters, Filters.MIN_PRICE_TO_SELL);
        
        public static decimal GetMinPriceToBuy(this KeyValuePair<string, string>[] filters)
            => GetDecimal(filters, Filters.MIN_PRICE_TO_BUY);
        
        public static decimal GetMaxPriceToBuy(this KeyValuePair<string, string>[] filters)
            => GetDecimal(filters, Filters.MIN_PRICE_TO_BUY);

        public static IEnumerable<string> GetMintCountryNames(this KeyValuePair<string, string>[] filters) => filters
            .Where(f => f.Key == Filters.MINT_COUNTRY_NAME)
            .Select(f => f.Value);

        private static decimal GetDecimal(KeyValuePair<string, string>[] filters, string key) => filters
                .Where(f => f.Key == key)
                .Select(f => decimal.Parse(f.Value, CultureInfo.InvariantCulture))
                .SingleOrDefault();   
    }
}
