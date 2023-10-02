using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace GoldAggregator.Infrastructure.Repositories
{
    public class BaseRepository
    {

        public bool Flush(IConfiguration configuration, IMemoryCache memoryCache)
        {
            
            throw new NotImplementedException();
        }
        /// <summary>
        /// Get cached time helper
        /// </summary>
        /// <param name="type">Minutes | Hours | Seconds</param>
        /// <returns></returns>
        protected TimeSpan GetCacheTime(IConfiguration configuration, string CacheKey)
        {
            var value = configuration.GetValue<string>($"{CacheKey}:Time");
            var cacheTime = TimeSpan.Parse(value);

            return cacheTime;
        }
    }
}
