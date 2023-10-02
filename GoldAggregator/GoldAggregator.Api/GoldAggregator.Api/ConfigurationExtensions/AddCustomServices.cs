using GoldAggregator.Infrastructure.Repositories;
using GoldAggregator.Infrastructure.Repositories.Cache;
using GoldAggregator.Infrastructure.Repositories.PgSql;
using GoldAggregator.Parser.Services.Abstractions;
using GoldAggregator.Parser.Services;

namespace GoldAggregator.Api.ConfigurationExtensions
{
    public static class AddCustomServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, bool useCache = false)
        {
            if (useCache)
            {
                services.AddTransient<ISeoService, CashedSeoService>();
            }
            else
            {
                services.AddTransient<ISeoService, SeoService>();
            }

            return services;
        }
    }
}
