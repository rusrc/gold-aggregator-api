using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Managers;
using GoldAggregator.Parser.Provider;
using GoldAggregator.Parser.Provider.Map;
using GoldAggregator.Parser.Provider.Realization;
using GoldAggregator.Parser.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Npgsql;

namespace GoldAggregator.Parser.Console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // TODO SetSwitch https://www.npgsql.org/doc/types/datetime.html
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var services = new ServiceCollection();
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();

            services.AddTransient<IConfiguration>(c => config);
            services.AddLogging(c => c.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));

            var logger = services.BuildServiceProvider().GetService<ILogger<Program>>();
            var options = GetDbContextOptions("ParserDbContext", logger);

            services.AddSingleton(x => new ParserDbContext(options));
            services.AddHttpClient();
            services.AddTransient<IHttpClient, HttpBaseClient>();

            //Подставляем сюда класс провайдера для тестирования ZolotoMdRuProvider /  MonetaInvestSpbProvider  и т.д
            services.AddTransient<ISiteParsingProvider, MonetaInvestSpbProvider>();
            services.AddTransient<SaveUrlsManager>();
            services.AddTransient<SaveCollectionManager>();
            services.AddTransient<DefaultMap>();


            try
            {
                // new SeoService().GetTitle();

                var provider = services.BuildServiceProvider();
                var saveUrlsManager = provider.GetService<SaveUrlsManager>();
                var saveItemsManager = provider.GetService<SaveCollectionManager>();

                CancellationTokenSource cancelTokenSource = new();

                await RunGetUrlsAsync(saveUrlsManager, logger, cancelTokenSource.Token);
                await RunGetItemsAsync(saveItemsManager, logger, cancelTokenSource.Token);

                System.Console.OutputEncoding = System.Text.Encoding.UTF8;
                System.Console.WriteLine("All finished");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message, ex);
            }
        }

        static public async Task RunGetUrlsAsync(ISaveUrlsManager saveUrlsManager, ILogger logger, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Starting get urls...");

            await saveUrlsManager.RunAsync(cancellationToken);

            logger.LogInformation("End get urls...");
        }

        static public async Task RunGetItemsAsync(ISaveItemsManager saveItemsManager, ILogger logger, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Starting get items...");

            await saveItemsManager.RunAsync(cancellationToken);

            logger.LogInformation("End get items...");
        }

        static private DbContextOptions<ParserDbContext> GetDbContextOptions(string dbContext, ILogger logger)
        {
            var connectionString = $"Host=188.134.85.64;Port=5432;Database={dbContext};Username=postgres;Password=******;Maximum Pool Size=100";
            var connection = new NpgsqlConnection(connectionString);

            connection.StateChange += (sender, args) =>
            {
                if (args.OriginalState == ConnectionState.Open)
                {
                    logger.LogDebug("Npgsql ConnectionState.Open ....");
                }
                else
                {
                    logger.LogDebug("Npgsql ConnectionState: " + args.OriginalState);
                }
            };

            var options = new DbContextOptionsBuilder<ParserDbContext>()
                .UseNpgsql(connection, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);

                }).EnableSensitiveDataLogging(true)
            .Options;

            return options;
        }

        static private DbContextOptions<ParserDbContext> GetInMemoryDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<ParserDbContext>()
             .UseInMemoryDatabase("ParserDbContext")
            .Options;

            return options;
        }
    }
}
