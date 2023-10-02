using GoldAggregator.Api.Dto;
using GoldAggregator.Api.Dto.Enums;
using GoldAggregator.Infrastructure.Repositories.PgSql;
using GoldAggregator.Parser.DbContext;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoldAggregator.Infrastructure.Repositories.Cache
{
    public class CachedCoinPriceRepository : CoinPriceRepository
    {
        const string CacheKey = "MemaryCaching:For:CoinPricesKey";

        private readonly ILogger<CachedCoinPriceRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CachedCoinPriceRepository(
            ILogger<CachedCoinPriceRepository> logger,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ParserDbContext context)
            : base(logger, context)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public override async Task<PagedListDto<CoinCatalogDto>> GetPagedCoinPricesAsync(
            KeyValuePair<string, string>[] filters, 
            int? page = 1, 
            int? pageSize = 10,
            SortCatalogCoins sortBy = SortCatalogCoins.None,
            SortDirection direction = SortDirection.Asc)
        {
            // Unique key to cache for some time
            var cacheKey = filters
                .Select(f => $"{f.Key}-{f.Value}")
                .Aggregate((prev, next) => $"{prev}:{next}") + $":page={page}+pageSize={pageSize}+sortBy={sortBy}+sortDir={direction}";

            if (!_memoryCache.TryGetValue<PagedListDto<CoinCatalogDto>>(cacheKey, out var cachedItems))
            {
                cachedItems = await base.GetPagedCoinPricesAsync(filters, page, pageSize, sortBy, direction);

                var relative = GetCacheTime();
                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(relative);
                _memoryCache.Set(cacheKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedItems;
        }

        private TimeSpan GetCacheTime()
        {
            var value = _configuration.GetValue<string>($"{CacheKey}:Time");
            var cacheTime = TimeSpan.Parse(value);

            return cacheTime;
        }
    }
}
