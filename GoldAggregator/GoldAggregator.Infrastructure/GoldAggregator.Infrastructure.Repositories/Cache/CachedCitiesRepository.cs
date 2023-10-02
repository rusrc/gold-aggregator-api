using GoldAggregator.Infrastructure.Repositories.PgSql;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GoldAggregator.Infrastructure.Repositories.Cache
{
    public class CachedCitiesRepository : CitiesRepository
    {
        const string CitiesKey = "MemaryCaching:For:CitiesKey";
        const string CityKey = "MemaryCaching:For:CityKey";

        private readonly ILogger<CachedCitiesRepository> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        public CachedCitiesRepository(
            ILogger<CachedCitiesRepository> logger,
            IMemoryCache memoryCache,
            IConfiguration configuration,
            ParserDbContext context)
         : base(context)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        public override async Task<List<City>> GetCitiesAsync()
        {
            if (!_memoryCache.TryGetValue<List<City>>(CitiesKey, out var cachedItems))
            {
                cachedItems = await base.GetCitiesAsync();

                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(GetCacheTime(CitiesKey));
                _memoryCache.Set(CitiesKey, cachedItems, options);

                _logger.LogInformation($"[CACHE] [SET] for key {CitiesKey}");
                return cachedItems;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {CitiesKey}");
            return cachedItems;
        }

        public override async Task<City?> GetCityByIdAsync(int cityId)
        {
            var cacheKey = GetCityCacheKey(cityId);

            if (!_memoryCache.TryGetValue<City>(cacheKey, out var cachedCity))
            {
                cachedCity = await base.GetCityByIdAsync(cityId);

                var options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(GetCacheTime(CityKey));
                _memoryCache.Set(cacheKey, cachedCity, options);

                _logger.LogInformation($"[CACHE] [SET] for key {cacheKey}");
                return cachedCity;
            }

            _logger.LogInformation($"[CACHE] [ACTIVE] for key {cacheKey}");
            return cachedCity;
        }

        private TimeSpan GetCacheTime(string cacheKey)
        {
            var value = _configuration.GetValue<string>($"{cacheKey}:Time");
            var cacheTime = TimeSpan.Parse(value);

            return cacheTime;
        }

        private string GetCityCacheKey(int cityId)
        {
            return $"{CityKey}:{cityId}";
        }
    }
}
