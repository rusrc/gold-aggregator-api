using GoldAggregator.Api.Dto;
using GoldAggregator.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GoldAggregator.Parser.Services
{
    public class CashedSeoService : SeoService
    {
        const string Key = "MemaryCaching:For:SeoServiceKey";
        private readonly JsonLdService _jsonLdService;
        private readonly ICitiesRepository _citiesRepository;
        private readonly ICoinPriceRepository _coinPriceRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CashedSeoService> _logger;
        private readonly IConfiguration _configuration;

        public CashedSeoService(
            JsonLdService jsonLdService,
            ICitiesRepository citiesRepository,
            ICoinPriceRepository coinPriceRepository,
            IMemoryCache memoryCache,
            ILogger<CashedSeoService> logger,
            IConfiguration configuration)
            : base(jsonLdService, citiesRepository, coinPriceRepository)
        {
            _jsonLdService = jsonLdService;
            _citiesRepository = citiesRepository;
            _coinPriceRepository = coinPriceRepository;
            _memoryCache = memoryCache;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<DtoSeo> GetSeoByFiltersAsync(KeyValuePair<string, string>[] filters)
        {
            // Unique key to cache for some time
            var cacheKey = $"{Key}_GetSeoByFiltersAsync" + filters
                .Select(f => $"{f.Key}-{f.Value}")
                .Aggregate((prev, next) => $"{prev}:{next}");

            if (!_memoryCache.TryGetValue<DtoSeo>(cacheKey, out var cachedItems))
            {
                cachedItems = await base.GetSeoByFiltersAsync(filters);

                var relative = GetCacheTime();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
                _memoryCache.Set(cacheKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedItems;
        }

        public override async Task<DtoSeo> GetSeoByFiltersAsync(KeyValuePair<string, string>[] filters, IEnumerable<CoinCatalogDto> coinCatalogDtos)
        {
            // Unique key to cache for some time
            var cacheKey = $"{Key}_GetSeoByFiltersAsync" + filters
                .Select(f => $"{f.Key}-{f.Value}")
                .Aggregate((prev, next) => $"{prev}:{next}") +
                coinCatalogDtos
                .Select(c => $"{c.Name}")
                .Aggregate((prev, next) => $"{prev}:{next}");

            if (!_memoryCache.TryGetValue<DtoSeo>(cacheKey, out var cachedItems))
            {
                cachedItems = await base.GetSeoByFiltersAsync(filters, coinCatalogDtos);

                var relative = GetCacheTime();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
                _memoryCache.Set(cacheKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedItems;
        }

        // Вкладка "Все"
        public override async Task<DtoSeo> GetCoinPageSeoForAllAsync(int coinCatalogId, int cityId = 0)
        {
            // Unique key to cache for some time
            var cacheKey = $"{Key}_GetCoinPageSeoForAllAsync_{coinCatalogId}_{cityId}" ;

            if (!_memoryCache.TryGetValue<DtoSeo>(cacheKey, out var cachedItems))
            {
                cachedItems = await base.GetCoinPageSeoForAllAsync(coinCatalogId, cityId);

                var relative = GetCacheTime();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
                _memoryCache.Set(cacheKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedItems;
        }

        // Вкладка "Продажи"
        public override async Task<DtoSeo> GetCoinPageSeoForSellAsync(int coinCatalogId, int cityId = 0)
        {
            // Unique key to cache for some time
            var cacheKey = $"{Key}_GetCoinPageSeoForSellAsync_{coinCatalogId}_{cityId}";

            if (!_memoryCache.TryGetValue<DtoSeo>(cacheKey, out var cachedItems))
            {
                cachedItems = await base.GetCoinPageSeoForSellAsync(coinCatalogId, cityId);

                var relative = GetCacheTime();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
                _memoryCache.Set(cacheKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedItems;
        }

        // Вкладка "Покупка"
        public override async Task<DtoSeo> GetCoinPageSeoForBuyAsync(int coinCatalogId, int cityId = 0)
        {
            // Unique key to cache for some time
            var cacheKey = $"{Key}GetCoinPageSeoForBuyAsync_{coinCatalogId}_{cityId}";

            if (!_memoryCache.TryGetValue<DtoSeo>(cacheKey, out var cachedItems))
            {
                cachedItems = await base.GetCoinPageSeoForBuyAsync(coinCatalogId, cityId);

                var relative = GetCacheTime();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
                _memoryCache.Set(cacheKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedItems;
        }

        public string GetHomeTitle()
        {
            return "Купить или продать монеты по выгодной цене";
        }
        private TimeSpan GetCacheTime()
        {
            var value = _configuration.GetValue<string>($"{Key}:Time");
            var cacheTime = TimeSpan.Parse(value);

            return cacheTime;
        }

    }

}
