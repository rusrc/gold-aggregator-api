using Microsoft.EntityFrameworkCore;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class CatalogSeeding
    {


        public static void SeedCatalog(this ModelBuilder builder)
        {
            #region GoldPopular
            var goldPopularCoins = new[]
            {
                new CoinCatalog()
                {
                   Id = 1,
                   Name ="Георгий Победоносец",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=22,
                   Weight= 31.1m,
                   Nomination ="200 рублей"
                },
                new CoinCatalog()
                {
                   Id = 2,
                   Name ="Американский Буффало",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 3,
                   Name ="Венский Филармоникер",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=2,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 4,
                   Name ="Австраллийский Кенгуру",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 5,
                   Name ="Кленовый лист",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=9,
                   Weight=31.1m
                },             
                 new CoinCatalog()
                {
                  Id = 6,
                   Name ="Американский Орел",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 7,
                   Name ="Британия",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=5,
                   Weight=31.1m
                },
                    new CoinCatalog()
                {
                  Id = 8,
                   Name ="Крюгерранд",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=34,
                   Weight=31.1m
                },
                    new CoinCatalog()
                {
                  Id = 9,
                   Name ="Австраллийский Лунар",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 10,
                   Name ="Панда",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=10,
                   Weight=31.1m
                },
                 new CoinCatalog()
                {
                   Id = 11,
                   Name ="Георгий Победоносец",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=22,
                   Weight=15.55m
                },
                new CoinCatalog()
                {
                   Id = 12,
                   Name ="Американский Буффало",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=15.55m
                },
                new CoinCatalog()
                {
                  Id = 13,
                   Name ="Венский Филармоникер",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=2,
                   Weight=15.55m
                },
                new CoinCatalog()
                {
                  Id = 14,
                   Name ="Австраллийский Кенгуру",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=15.55m
                },
                new CoinCatalog()
                {
                  Id = 15,
                   Name ="Кленовый лист",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=9,
                   Weight=15.55m
                },
                 new CoinCatalog()
                {
                  Id = 16,
                   Name ="Американский Орел",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=15.55m
                },
                  new CoinCatalog()
                {
                  Id = 17,
                   Name ="Британия",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=5,
                   Weight=15.55m
                },
                    new CoinCatalog()
                {
                  Id = 18,
                   Name ="Крюгерранд",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=34,
                   Weight=15.55m
                },
                    new CoinCatalog()
                {
                  Id = 19,
                   Name ="Австраллийский Лунар",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=15.55m
                },
                 new CoinCatalog()
                {
                  Id = 20,
                   Name ="Панда",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=10,
                   Weight=15.55m
                },
                 new CoinCatalog()
                {
                   Id = 21,
                   Name ="Георгий Победоносец",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=22,
                   Weight=7.78m
                },
                new CoinCatalog()
                {
                   Id = 22,
                   Name ="Американский Буффало",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=7.78m
                },
                new CoinCatalog()
                {
                  Id = 23,
                   Name ="Венский Филармоникер",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=2,
                   Weight=7.78m
                },
                new CoinCatalog()
                {
                  Id = 24,
                   Name ="Австраллийский Кенгуру",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=7.78m
                },
                new CoinCatalog()
                {
                  Id = 25,
                   Name ="Кленовый лист",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=9,
                   Weight=7.78m
                },
                 new CoinCatalog()
                {
                  Id = 26,
                   Name ="Американский Орел",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=7.78m
                },
                  new CoinCatalog()
                {
                  Id = 27,
                   Name ="Британия",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=5,
                   Weight=7.78m
                },
                    new CoinCatalog()
                {
                  Id = 28,
                   Name ="Крюгерранд",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=34,
                   Weight=7.78m
                },
                    new CoinCatalog()
                {
                  Id = 29,
                   Name ="Австраллийский Лунар",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=7.78m
                },
                 new CoinCatalog()
                {
                  Id = 30,
                   Name ="Панда",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=10,
                   Weight=7.78m
                },
                new CoinCatalog()
                {
                   Id = 41,
                   Name ="Георгий Победоносец",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=22,
                   Weight=3.11m
                },
                new CoinCatalog()
                {
                   Id = 42,
                   Name ="Американский Буффало",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=3.11m
                },
                new CoinCatalog()
                {
                  Id = 43,
                   Name ="Венский Филармоникер",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=2,
                   Weight=3.11m
                },
                new CoinCatalog()
                {
                  Id = 44,
                   Name ="Австраллийский Кенгуру",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=3.11m
                },
                new CoinCatalog()
                {
                  Id = 45,
                   Name ="Кленовый лист",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=9,
                   Weight=3.11m
                },
                 new CoinCatalog()
                {
                  Id = 46,
                   Name ="Американский Орел",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=27,
                   Weight=3.11m
                },
                  new CoinCatalog()
                {
                  Id = 47,
                   Name ="Британия",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=5,
                   Weight=3.11m
                },
                    new CoinCatalog()
                {
                  Id = 48,
                   Name ="Крюгерранд",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=34,
                   Weight=3.11m
                },
                    new CoinCatalog()
                {
                  Id = 49,
                   Name ="Австраллийский Лунар",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=1,
                   Weight=3.11m
                },
                 new CoinCatalog()
                {
                  Id = 50,
                   Name ="Панда",
                   MetalType =MetalTypeEnum.Gold,
                   MintCountryId=10,
                   Weight=3.11m
                },
             };

            //foreach (var item in goldPopularCoins)
            //{
            //    item.TranslitName = Transliteration.Generate(item.Name);

            //    var startName = Transliteration.Generate($"{item.MetalType.GetMetalTypeNounName()} монета");
            //    var seoUrl = $"{startName}_{item.TranslitName}";
                
            //    item.SeoUrl = seoUrl;
            //};

            builder.Entity<CoinCatalog>().HasData(goldPopularCoins);
            #endregion

            #region SilverPopular
            var silverPopularCoins = new[]
            {
                new CoinCatalog()
                {
                   Id = 51,
                   Name ="Георгий Победоносец",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=22,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                   Id = 52,
                   Name ="Американский Буффало",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=27,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 53,
                   Name ="Венский Филармоникер",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=2,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 54,
                   Name ="Австраллийский Кенгуру",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=1,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 55,
                   Name ="Кленовый лист",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=9,
                   Weight=31.1m
                },
                 new CoinCatalog()
                {
                  Id = 56,
                   Name ="Американский Орел",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=27,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 57,
                   Name ="Британия",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=5,
                   Weight=31.1m
                },
                    new CoinCatalog()
                {
                  Id = 58,
                   Name ="Крюгерранд",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=34,
                   Weight=31.1m
                },
                    new CoinCatalog()
                {
                  Id = 59,
                   Name ="Австраллийский Лунар",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=1,
                   Weight=31.1m
                },
                 new CoinCatalog()
                {
                  Id = 60,
                   Name ="Панда",
                   MetalType =MetalTypeEnum.Silver,
                   MintCountryId=10,
                   Weight=31.11m
                },
             };

            //foreach (var item in silverPopularCoins)
            //{
            //    item.TranslitName = Transliteration.Generate(item.Name);

            //    var startName = Transliteration.Generate($"{item.MetalType.GetMetalTypeNounName()} монета");
            //    var seoUrl = $"{startName}_{item.TranslitName}";

            //    item.SeoUrl = seoUrl;
           
            //};

            builder.Entity<CoinCatalog>().HasData(silverPopularCoins);
            #endregion

            #region PlatinumPopular
            var platinumPopularCoins = new[]
            {  
                new CoinCatalog()
                {
                  Id = 61,
                   Name ="Венский Филармоникер",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=2,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 62,
                   Name ="Австраллийский Кенгуру",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=1,
                   Weight=31.1m
                },
                new CoinCatalog()
                {
                  Id = 63,
                   Name ="Кленовый лист",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=9,
                   Weight=31.1m
                },
                 new CoinCatalog()
                {
                  Id = 64,
                   Name ="Американский Орел",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=27,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 65,
                   Name ="Британия",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },              
                    new CoinCatalog()
                {
                  Id = 66,
                   Name ="Утконос",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=1,
                   Weight=31.1m
                },
                 new CoinCatalog()
                {
                  Id = 67,
                   Name ="Королевский Герб",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 68,
                   Name ="Белая лошадь Ганновера",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 69,
                   Name ="Белый Грейхаунд Ричмонда",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 70,
                   Name ="Звери королевы",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 71,
                   Name ="Белый лев Мортимера",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 72,
                   Name ="Королевский Единорог",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=5,
                   Weight=31.1m
                },
                  new CoinCatalog()
                {
                  Id = 73,
                   Name ="Олимпийские игры",
                   MetalType =MetalTypeEnum.Platinum,
                   MintCountryId=28,
                   Weight=15.55m
                },               
             };

            //foreach (var item in platinumPopularCoins)
            //{
            //    item.TranslitName = Transliteration.Generate(item.Name);

            //    var startName = Transliteration.Generate($"{item.MetalType.GetMetalTypeNounName()} монета");
            //    var seoUrl = $"{startName}_{item.TranslitName}";

            //    item.SeoUrl = seoUrl;
            //};

            builder.Entity<CoinCatalog>().HasData(platinumPopularCoins);
            #endregion
        }
    }
}