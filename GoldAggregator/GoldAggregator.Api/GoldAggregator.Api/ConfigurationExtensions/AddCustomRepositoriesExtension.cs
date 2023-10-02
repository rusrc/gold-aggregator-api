using GoldAggregator.Infrastructure.Repositories;
using GoldAggregator.Infrastructure.Repositories.Cache;
using GoldAggregator.Infrastructure.Repositories.PgSql;

namespace GoldAggregator.Api.ConfigurationExtensions
{
    public static class AddCustomRepositoriesExtension
    {
        public static IServiceCollection AddCustomRepositories(this IServiceCollection services, bool useCache = false)
        {
            if (useCache)
            {
                services.AddTransient<IFiltersRepository, CachedFiltersRepository>();
                services.AddTransient<ICitiesRepository, CachedCitiesRepository>();
                services.AddTransient<ICoinPriceRepository, CachedCoinPriceRepository>();
                services.AddTransient<IAnalyticsRepository, CachedAnalyticsRepository>();
            }
            else
            {
                services.AddTransient<IFiltersRepository, FiltersRepository>();
                services.AddTransient<ICitiesRepository, CitiesRepository>();
                services.AddTransient<ICoinPriceRepository, CoinPriceRepository>();
                services.AddTransient<IAnalyticsRepository, AnalyticsRepository>();
            }

            return services;
        }
    }
}
