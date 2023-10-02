using GoldAggregator.Parser.DbContext;

using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Api.ConfigurationExtensions
{
    public static class AddCustomParserDbContextExtension
    {
        public enum ConnectionTypeEnum
        {
            /// <summary>
            /// Current default connection to laptop database
            /// </summary>
            Default,
            /// <summary>
            /// Use in memory
            /// </summary>
            InMemory,
            /// <summary>
            /// Connection string on dev PC
            /// </summary>
            Local,
            /// <summary>
            /// Connection for future cluster, I hope will be :)
            /// </summary>
            Cluster
        }

        public static IServiceCollection AddParserDbContext(this IServiceCollection services, ConnectionTypeEnum connectionType, IConfiguration configuration, string connectionString = "")
        {
            switch (connectionType)
            {
                case ConnectionTypeEnum.Default:
                    {
                        return services.AddDbContext<ParserDbContext>(options
                            => options
                            // .EnableSensitiveDataLogging()
                            .UseNpgsql(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure();
                            }));
                    }
                case ConnectionTypeEnum.InMemory:
                    {
                        return services.AddDbContext<ParserDbContext>(options
                            => options.UseInMemoryDatabase(databaseName: nameof(ParserDbContext)));
                    }
                case ConnectionTypeEnum.Local:
                    {
                        return services.AddDbContext<ParserDbContext>(options
                          => options
                          .EnableSensitiveDataLogging()
                          .UseNpgsql(configuration.GetConnectionString("LocalConnection"), sqlOptions =>
                          {
                              sqlOptions.EnableRetryOnFailure();
                          }));
                    }
                case ConnectionTypeEnum.Cluster:
                    throw new NotImplementedException();
                default:

                    if (string.IsNullOrEmpty(connectionString))
                    {
                        throw new ArgumentException($"{nameof(connectionString)} is empty");
                    }

                    return services.AddDbContext<ParserDbContext>(options
                        => options
                        .EnableSensitiveDataLogging()
                        .UseNpgsql(connectionString, sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure();
                        }));
            }
        }
    }
}
