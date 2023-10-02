using GoldAggregator.Api.Dto;
using GoldAggregator.Infrastructure.Repositories;
using GoldAggregator.Parcer.Entities.Extensions;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Extension;
using GoldAggregator.Parser.Services.Abstractions;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Services
{
    public class SeoService : ISeoService
    {
        private readonly JsonLdService _jsonLdService;
        private readonly ICitiesRepository _citiesRepository;
        private readonly ICoinPriceRepository _coinPriceRepository;

        public SeoService(
            JsonLdService jsonLdService,
            ICitiesRepository citiesRepository,
            ICoinPriceRepository coinPriceRepository)
        {
            _jsonLdService = jsonLdService;
            _citiesRepository = citiesRepository;
            _coinPriceRepository = coinPriceRepository;
        }

        public virtual async Task<DtoSeo> GetSeoByFiltersAsync(KeyValuePair<string, string>[] filters)
        {
            // var items = _coinPriceRepository.GetPagedCoinPricesAsync(filters, page, pageSize);

            var structure = new TitleFilterStructure(filters);
            var cityId = filters.GetCityId();

            if (cityId > 0)
            {
                var city = await _citiesRepository.GetCityByIdAsync(cityId);
                var title = string.Format(structure.ToString(), city.Name);
                var description = $"На нашем сайте, вы можете узнать {title.ToLowerFirstLetter()}. Удобное сравнение цен в {city.Name} на сайте Голд Каталог (www.goldkatalog.ru).";
                return new DtoSeo
                {
                    Title = title,
                    Description = description,
                    // TODO json-ld
                    JsonLd = null // _jsonLdService.HomeCoinsPageByFilters(filters)
                };
            }
            else
            {
                var title = structure.ToString();
                // TODO description
                var description = $"На нашем сайте, вы можете узнать {title.ToLowerFirstLetter()}. Удобное сравнение цен в регионе или стране на сайте Голд Каталог (www.goldkatalog.ru).";
                return new DtoSeo
                {
                    Title = title,
                    Description = description,
                    // TODO json-ld
                    JsonLd = null // _jsonLdService.HomeCoinsPageByFilters(filters)
                };
            }
        }

        public virtual async Task<DtoSeo> GetSeoByFiltersAsync(KeyValuePair<string, string>[] filters, IEnumerable<CoinCatalogDto> coinCatalogDtos)
        {
            var structure = new TitleFilterStructure(filters);
            var cityId = filters.GetCityId();

            if (cityId > 0)
            {
                var city = await _citiesRepository.GetCityByIdAsync(cityId);
                var title = string.Format(structure.ToString(), city.Name);
                var description = structure.ToString();
                return new DtoSeo
                {
                    Title = title,
                    Description = structure.ToString(),
                    JsonLd = _jsonLdService.HomeCoinsPageByFilters(title, description, coinCatalogDtos)
                };
            }
            else
            {
                var title = structure.ToString();
                var description = structure.ToString();
                return new DtoSeo
                {
                    Title = title,
                    Description = structure.ToString(),
                    JsonLd = _jsonLdService.HomeCoinsPageByFilters(title, description, coinCatalogDtos)
                };
            }
        }

        // Вкладка "Все"
        public virtual async Task<DtoSeo> GetCoinPageSeoForAllAsync(int coinCatalogId, int cityId = 0)
        {
            var coin = await _coinPriceRepository.GetCoinCatalogWithPricesAsync(coinCatalogId, cityId);
            var metalType = (MetalTypeEnum)coin.MetalType;

            var city = await _citiesRepository.GetCityByIdAsync(cityId);
            var location = city is not null ? $"в {city.Name}" : "по России";

            var seoTitle = $"Купить или продать {metalType.GetMetalAccusativeName()} монету '{coin.Name}' {coin.Weight} г. {location}";
            var seoDescription = $"Лучшие цены {location}, " +
                    $"на {metalType.GetMetalAccusativeName()} монету '{coin.Name}', " +
                    $"можно купить от {coin.MinPriceToBuy.ToRublePrice()} руб., " +
                    $"цена продажи до {coin.MaxPriceToSell.ToRublePrice()} руб. " +
                    $"Весь перечень цен на нашем сайте goldkatalog.ru";

            string title = $"Цены на монету '{coin.Name}' (от {coin.MinPriceToSell.ToRublePrice()} - до {coin.MaxPriceToSell.ToRublePrice()})";
            string description = $"Представлен подробный список текущих цен на монету '{coin.Name}' {location}";

            return new DtoSeo
            {
                Title = title,
                Description = description,
                seoTitle = seoTitle,
                seoDescription = seoDescription,
                JsonLd = _jsonLdService.CoinCatalogPage(seoTitle, seoDescription, coin)
            };
        }

        // Вкладка "Продажи"
        public virtual async Task<DtoSeo> GetCoinPageSeoForSellAsync(int coinCatalogId, int cityId = 0)
        {
            var coin = await _coinPriceRepository.GetCoinCatalogWithPricesAsync(coinCatalogId, cityId);
            var metalType = (MetalTypeEnum)coin.MetalType;

            var city = await _citiesRepository.GetCityByIdAsync(cityId);
            var location = city is not null ? $"в {city.Name}" : "по России";

            var seoTitle = $"Продать {metalType.GetMetalAccusativeName()} монету '{coin.Name}' {coin.Weight} г. {location}";
            var seoDescription = $"Лучшие цены {location}, " +
                    $"на {metalType.GetMetalAccusativeName()} монету '{coin.Name}', " +
                    $"цена продажи до {coin.MaxPriceToSell.ToRublePrice()} руб. " +
                    $"Весь перечень цен на нашем сайте goldkatalog.ru";

            string title = $"Цены на монету '{coin.Name}' (от {coin.MinPriceToSell.ToRublePrice()} - до {coin.MaxPriceToSell.ToRublePrice()})";
            string description = $"Представлен подробный список текущих цен у дилеров, где можно продать монету '{coin.Name}' {location}";

            return new DtoSeo
            {
                Title = title,
                Description = description,
                seoTitle = seoTitle,
                seoDescription = seoDescription,
                JsonLd = _jsonLdService.CoinCatalogPage(seoTitle, seoDescription, coin)
            };
        }

        // Вкладка "Покупка"
        public virtual async Task<DtoSeo> GetCoinPageSeoForBuyAsync(int coinCatalogId, int cityId = 0)
        {
            var coin = await _coinPriceRepository.GetCoinCatalogWithPricesAsync(coinCatalogId, cityId);
            var metalType = (MetalTypeEnum)coin.MetalType;

            var city = await _citiesRepository.GetCityByIdAsync(cityId);
            var location = city is not null ? $"в {city.Name}" : "по России";

            var seoTitle = $"Купить {metalType.GetMetalAccusativeName()} монету '{coin.Name}' {coin.Weight} г. {location}";
            var seoDescription = $"Лучшие цены {location}, " +
                    $"на {metalType.GetMetalAccusativeName()} монету '{coin.Name}', " +
                    $"можно купить от {coin.MinPriceToBuy.ToRublePrice()} руб., " +
                    $"Весь перечень цен на нашем сайте goldkatalog.ru";

            string title = $"Цены на монету '{coin.Name}' (от {coin.MinPriceToBuy.ToRublePrice()} - до {coin.MaxPriceToBuy.ToRublePrice()})";
            string description = $"Представлен подробный список текущих цен у дилеров, где можно купить монету '{coin.Name}' {location}.";

            return new DtoSeo
            {
                Title = title,
                Description = description,
                seoTitle = seoTitle,
                seoDescription = seoDescription,
                JsonLd = _jsonLdService.CoinCatalogPage(seoTitle, seoDescription, coin)
            };
        }

        public string GetHomeTitle()
        {
            return "Купить или продать монеты по выгодной цене";
        }

    }

    internal class TitleFilterStructure
    {
        private readonly KeyValuePair<string, string>[] _filters;

        public TitleFilterStructure(KeyValuePair<string, string>[] filters)
        {
            _filters = filters;
        }

        public string Weights
        {
            get
            {
                if (_filters.GetWeights().Any())
                    return _filters.GetWeights()
                        .Select(w => $"{w} г").Aggregate((prev, next) => $"{prev}, {next}");
                else
                    return "";
            }
        }

        public string Metals
        {
            get
            {
                if (_filters.GetMetalTypes().Any())
                    return _filters.GetMetalTypes()
                          .Select(w => ((MetalTypeEnum)w).GetMetalPluralNounName().ToLower())
                          .Aggregate((prev, next) => $"{prev}, {next}");

                else
                    return null;
            }
        }

        public int CityId { get => _filters.GetCityId(); }

        public override string ToString()
        {
            var visitor = new TitleFilterVisitor();
            visitor.VisitMethodCall(this);

            return visitor.ToString();
        }

        public void Test()
        {
            var visitor = new TitleFilterVisitor();
            visitor.VisitMethodCall(this);

            //_sb.Append("Где купить ");
            //_sb.Append($" {Metals} монеты ");
            //_sb.Append($"весом {Weights} г. ");

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(visitor.ToString());
        }
    }

    internal class TitleFilterVisitor
    {
        private StringBuilder _sb = new StringBuilder();

        // Где купить инвестиционные монеты в России
        // Купить золотые монеты в России
        // Где купить инвестиционные монеты, весом 3.11 г. в России
        // Купить золотые монеты, весом 3.11 г в России

        public void VisitMethodCall(TitleFilterStructure structure)
        {
            this.VisitInitial(structure);
            this.VisitMetalType(structure);
            this.VisitWeight(structure);
            this.VisitCity(structure);
        }

        public void VisitInitial(TitleFilterStructure structure)
        {
            if (structure.Metals.IsNotEmpty() && structure.Weights.IsNotEmpty())
                _sb.Append("Купить ");
            else
                _sb.Append("Где купить инвестиционные ");
        }

        public void VisitWeight(TitleFilterStructure structure)
        {
            if (structure.Weights.IsNotEmpty())
                _sb.Append($" весом {structure.Weights} ");
        }

        public void VisitMetalType(TitleFilterStructure structure)
        {
            if (structure.Metals.IsNotEmpty())
                _sb.Append($" {structure.Metals} монеты ");
            else
                _sb.Append(" монеты ");
        }

        public void VisitMinPriceToSell() { }

        public void VisitMaxPriceToSell() { }

        public void VisitMinPriceToBuy() { }

        public void VisitMaxPriceToBuy() { }

        public void VisitCity(TitleFilterStructure structure)
        {
            if (structure.CityId != 0)
                _sb.Append($" в {{{0}}}");
            else
                _sb.Append($" в России");

        }

        public override string ToString()
        {
            var sentence = _sb.ToString();
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[\\s]{2,}", options);

            sentence = regex.Replace(sentence, " ");

            return sentence;
        }
    }

}
