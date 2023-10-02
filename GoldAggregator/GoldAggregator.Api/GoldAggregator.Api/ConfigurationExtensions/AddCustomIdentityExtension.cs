using GoldAggregator.Parser.DbContext;

using Microsoft.AspNetCore.Identity;

namespace GoldAggregator.Api.ConfigurationExtensions
{
    public static class AddCustomIdentityExtension
    {
        /// <summary>
        /// Добавил Identity не проверял рабботу, см. ссылку https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio
        /// </summary>
        public static IServiceCollection AddIdentityForParserDbContext(this IServiceCollection services)
        {
            services
                .AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ParserDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });



            return services;
        }
    }
}
