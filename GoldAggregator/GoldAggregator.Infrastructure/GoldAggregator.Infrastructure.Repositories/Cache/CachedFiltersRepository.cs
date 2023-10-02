using GoldAggregator.Api.Dto;
using GoldAggregator.Infrastructure.Repositories.PgSql;
using GoldAggregator.Parser.DbContext;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoldAggregator.Infrastructure.Repositories.Cache
{
    // IMemoryCache https://docs.microsoft.com/ru-ru/aspnet/core/performance/caching/memory?view=aspnetcore-6.0
    public class CachedFiltersRepository : FiltersRepository
    {
        const string CacheKey = "MemaryCaching:For:FiltersKey";

        private readonly ILogger<CachedFiltersRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CachedFiltersRepository(
            ILogger<CachedFiltersRepository> logger,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ParserDbContext context)
            : base(context)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public override async Task<KeyLabelValuePair[]> GetFiltersAsync()
        {
            if (!_memoryCache.TryGetValue<KeyLabelValuePair[]>(CacheKey, out var cachedfilters))
            {
                cachedfilters = await base.GetFiltersAsync();

                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(GetCacheTime());
                _memoryCache.Set(CacheKey, cachedfilters, options);

                _logger.LogInformation($"[CACHE] [SET] for key {CacheKey}");
                return cachedfilters;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {CacheKey}");
            return cachedfilters;
        }

        private TimeSpan GetCacheTime()
        {
            var value = _configuration.GetValue<string>($"{CacheKey}:Time");
            var cacheTime = TimeSpan.Parse(value);

            return cacheTime;
        }
    }
}
