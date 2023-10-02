using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Managers;
using GoldAggregator.Parser.Provider.Map;
using GoldAggregator.Parser.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Npgsql;

namespace GoldAggregator.Parser.WorkerService.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddWorkerConfiguration(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddTransient<IConfiguration>(c => config);
            services.AddLogging(c => c
                    .AddConsole()
                    .AddDebug()
                    .SetMinimumLevel(LogLevel.Debug));

            var logger = services.BuildServiceProvider().GetService<ILogger>();
            var options = GetDbContextOptions("ParserDbContext");

            services.AddSingleton(x => new ParserDbContext(options));
            services.AddHttpClient();
            services.AddTransient<IHttpClient, HttpBaseClient>();
            services.AddTransient<ISaveUrlsManager, SaveUrlsManager>();
            services.AddTransient<ISaveItemsManager, SaveCollectionManager>();
            services.AddTransient<SaveUrlsManager>();
            services.AddTransient<SaveCollectionManager>();
            services.AddTransient<DefaultMap>();


            return services;
        }

        static DbContextOptions<ParserDbContext> GetDbContextOptions(string dbContext)
        {
            var connectionString = $"Host=188.134.85.64;Port=5432;Database={dbContext};Username=postgres;Password=*********;Maximum Pool Size=100";
            var connection = new NpgsqlConnection(connectionString);


            var options = new DbContextOptionsBuilder<ParserDbContext>()
                .UseNpgsql(connection, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

                }).EnableSensitiveDataLogging(true)
            .Options;

            return options;
        }
    }
}