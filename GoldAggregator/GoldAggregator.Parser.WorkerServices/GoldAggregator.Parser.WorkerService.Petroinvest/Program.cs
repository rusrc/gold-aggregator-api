using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Managers;
using GoldAggregator.Parser.Provider;
using GoldAggregator.Parser.Provider.Map;
using GoldAggregator.Parser.Provider.Realization;
using GoldAggregator.Parser.Services;
using GoldAggregator.Parser.WorkerService.ZolotoMdRu;

using Microsoft.EntityFrameworkCore;

using Npgsql;

using System.Data;

// TODO SetSwitch https://www.npgsql.org/doc/types/datetime.html
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

IServiceCollection services = new ServiceCollection();
var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

IHost host = Host.CreateDefaultBuilder(args).UseWindowsService()
    .ConfigureServices(services =>
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
        services.AddHostedService<Worker>();
        services.AddTransient<ISiteParsingProvider, PetroinvestProvider>();
        services.AddTransient<ISaveUrlsManager, SaveUrlsManager>();
        services.AddTransient<ISaveItemsManager, SaveCollectionManager>(); 
        services.AddTransient<SaveUrlsManager>();
        services.AddTransient<SaveCollectionManager>();
        services.AddTransient<DefaultMap>();
    })
    .Build();

await host.RunAsync();


DbContextOptions<ParserDbContext> GetDbContextOptions(string dbContext)
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