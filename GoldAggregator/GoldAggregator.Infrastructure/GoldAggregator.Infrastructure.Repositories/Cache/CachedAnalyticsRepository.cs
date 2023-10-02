using GoldAggregator.Api.Dto.Analytics;
using GoldAggregator.Infrastructure.Repositories.PgSql;
using GoldAggregator.Parser.DbContext;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoldAggregator.Infrastructure.Repositories.Cache
{
    public class CachedAnalyticsRepository : AnalyticsRepository, IAnalyticsRepository
    {
        const string Key = "MemaryCaching:For:AnalyticsKey";

        private readonly ILogger<CachedAnalyticsRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CachedAnalyticsRepository(
            ILogger<CachedAnalyticsRepository> logger,
            ParserDbContext context,
            IMemoryCache memoryCache,
            IConfiguration configuration) : base(logger, context)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public async Task<IEnumerable<DtoDynamicCoinPrice>> GetPriceToBuy(int coinCatalogId, int lastDays = 30)
        {
            var dynamicKey = $"{Key}+{nameof(coinCatalogId)}={coinCatalogId}+{nameof(lastDays)}={lastDays}";

            if (!_memoryCache.TryGetValue<IEnumerable<DtoDynamicCoinPrice>>(dynamicKey, out var cachedItems))
            {
                cachedItems = await base.GetPriceToBuy(coinCatalogId, lastDays);

                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(GetCacheTime());
                _memoryCache.Set(dynamicKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {dynamicKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {dynamicKey}");
            return cachedItems;
        }

        private TimeSpan GetCacheTime()
        {
            var value = _configuration.GetValue<string>($"{Key}:Time");

            return TimeSpan.Parse(value);
        }
    }
}
