using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.DbContext;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class DealerCoinMapSeeding
    {
        public static void SeedDealerCoinMap(this ModelBuilder builder)
        {
            var data = new[]
            {
                new DealerCoinMap()
                {
                   Id = 1,
                    DealerId=1,
                    MatchedTitle ="Золотая инвестиционная монета Австрии Венский Филармоникер, 31,1 гр чистого золота (проба 0,9999)",
                    CoinFromCatalogId = 3,
                    Url ="https://zoloto-md.ru/bullion-coins/i-inostrannyye/zolotaya-investiczionnaya-moneta-avstrijskij-filarmoniker",

                },
              new DealerCoinMap()
                {
                   Id = 2,
                    DealerId=1,
                    MatchedTitle ="Золотая монета Канады \"Кленовый Лист\", 31.1 г чистого золота (проба 0.9999)",
                    CoinFromCatalogId = 5,
                    Url ="https://zoloto-md.ru/bullion-coins/i-inostrannyye/zolotaya-moneta-kanadyi-klenovyij-list,-31.1-g-chistogo-zolota-proba-0.9999",
                },
                new DealerCoinMap()
                {
                   Id = 3,
                    DealerId=1,
                    MatchedTitle ="Золотая инвестиционная монета Георгий ПОБЕДОНОСЕЦ СПМД 2006 - 2012 г.в., вес чистого золота - 7.78 г (проба 0,999)",
                    CoinFromCatalogId = 1,
                    Url ="https://zoloto-md.ru/bullion-coins/i-rossiya-i-sssr/zolotaya-investiczionnaya-moneta-georgij-pobedonosecz-spmd",
                },
                new DealerCoinMap()
                {
                   Id = 4,
                    DealerId=1,
                    MatchedTitle ="Золотая инвестиционная монета Георгий Победоносец СПМД 2018-2022 г.в., 7.78 г чистого золота (проба 0,999)",
                    CoinFromCatalogId = 1,
                    Url ="https://zoloto-md.ru/bullion-coins/i-rossiya-i-sssr/zolotaya-investiczionnaya-moneta-georgij-pobedonosecz-mmd,-7,78-g-chistogo-zolota-proba-0,999",
                },
                new DealerCoinMap()
                {
                   Id = 5,
                    DealerId=1,
                    MatchedTitle ="Золотая инвестиционная монета Австралии - Кенгуру 2022 г.в., 31.1 г чистого золота (проба 0,9999)",
                    CoinFromCatalogId = 4,
                    Url ="https://zoloto-md.ru/bullion-coins/i-inostrannyye/zolotaya-investiczionnaya-moneta-avstralii-kenguru-2022-g.v.,-31.1-g-chistogo-zolota-proba-0,9999",
                },
             };
            builder.Entity<DealerCoinMap>().HasData(data);
        }
    }
}