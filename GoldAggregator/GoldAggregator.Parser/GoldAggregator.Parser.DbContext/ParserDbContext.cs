
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.DbContext.Seedings;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace GoldAggregator.Parser.DbContext
{
    public class ParserDbContext : IdentityDbContext
    {
        public ParserDbContext(DbContextOptions options) : base(options) {
            
        }
        public DbSet<CoinCatalog> CoinCatalogs { get; set; }
        public DbSet<CoinPrice> CoinPrices { get; set; }
        public DbSet<CoinPriceHistory> CoinsPriceHistory { get; set; }
        public DbSet<CoinPriceHistory> CoinPriceRedirects { get; set; }
        public DbSet<DealerCoinMap> DillerCoinMaps { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<DealerOffice> DealerOffices { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<MintCountry> MintCountries { get; set; }
        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add in a begining to rewrite table names
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(entity => entity.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(entity => entity.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
            modelBuilder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));

            modelBuilder.ApplyConfiguration(new UrlConfiguration()).SeedUrls();

            modelBuilder.ApplyConfiguration(new CoinConfiguration());
            modelBuilder.ApplyConfiguration(new CoinPriceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DealerConfiguration());
            modelBuilder.ApplyConfiguration(new DealerOfficeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogConfiguration());
            modelBuilder.ApplyConfiguration(new DealerCoinMapConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CoinPriceClickConfiguration());

            //SeedData
            SeedDefaultAdminUser(modelBuilder);
            modelBuilder.SeedDealers();
            modelBuilder.SeedDealerOffices();
            modelBuilder.SeedCatalog();
            modelBuilder.SeedDealerCoinMap();
            modelBuilder.SeedCities();
            modelBuilder.SeedCountries();
        }

        private static void SeedDefaultAdminUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "25723030-b8e5-4b2a-8c06-5cf170e3443b",
                UserName = "admin@mail.ru",
                NormalizedUserName = "ADMIN@MAIL.RU",
                Email = "admin@mail.ru",
                NormalizedEmail = "ADMIN@MAIL.RU",
                // 123456Ru!
                PasswordHash = "AQAAAAEAACcQAAAAEA7vpzNBUIMLvB4bdfb8xX5IIsMZ86GfG1In4YX3q8BYyZoFQYSGuVOVWB3XfCdZOA==",
                SecurityStamp = "6FPVSOIY6BCPOQH4BZUCNYIKQ5WB4VRM",
                ConcurrencyStamp = "9ac88e11-e377-4a49-bef9-ed7767ff6f9d"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                Name = "Admin",
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "d0c691d4-98dc-4ee6-b9db-633fcc9c238e",
                UserId = "25723030-b8e5-4b2a-8c06-5cf170e3443b"
            });
        }       
    }
}
