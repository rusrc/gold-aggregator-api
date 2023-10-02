
using GoldAggregator.Parser.Entities.Entities;
// using GoldAggregator.Parser.Services;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class CountriesSeeding
    {
        public static void SeedCountries(this ModelBuilder builder)
        {
            var data = new[]
            {
                new MintCountry()
                {
                   Id = 1,
                   Name ="Австралия",
                },
                new MintCountry()
                {
                   Id = 2,
                   Name ="Австрия",
                },
                new MintCountry()
                {
                   Id = 3,
                   Name ="Армения",
                },
                new MintCountry()
                {
                   Id = 4,
                   Name ="Беларусь",
                },
                new MintCountry()
                {
                   Id = 5,
                   Name ="Великобритания",
                },
                new MintCountry()
                {
                   Id = 6,
                   Name ="Германия",
                },
                new MintCountry()
                {
                   Id = 7,
                   Name ="Казахстан",
                },
                new MintCountry()
                {
                   Id = 8,
                   Name ="Камерун",
                },
                new MintCountry()
                {
                   Id =9 ,
                   Name ="Канада",
                },
                new MintCountry()
                {
                   Id = 10,
                   Name ="Китай",
                },
                  new MintCountry()
                 {
                   Id = 11,
                   Name ="Конго",
                },
                 new MintCountry()
                {
                   Id = 12,
                   Name ="Кыргыстан",
                },
                   new MintCountry()
                 {
                   Id = 13,
                   Name ="Либерия",
                },
                     new MintCountry()
                 {
                   Id = 14,
                   Name ="Малави",
                },
                 new MintCountry()
                {
                   Id = 15,
                   Name ="Мексика",
                },
                   new MintCountry()
                 {
                   Id = 16,
                   Name ="Монголия",
                },
                     new MintCountry()
                 {
                   Id = 17,
                   Name ="Науру",
                },
                       new MintCountry()
                 {
                   Id = 18,
                   Name ="Ниуэ",
                },
                  new MintCountry()
                 {
                   Id = 19,
                   Name ="Острова Мэн",
                },
                 new MintCountry()
                {
                   Id = 20,
                   Name ="Острова Кука",
                },
                   new MintCountry()
                 {
                   Id = 21,
                   Name ="Палау",
                },
                new MintCountry()
                {
                   Id = 22,
                   Name ="Россия",
                },
                  new MintCountry()
                 {
                   Id = 23,
                   Name ="Руанда",
                },
                    new MintCountry()
                 {
                   Id = 24,
                   Name ="Сент-Китс И Невис",
                },
                      new MintCountry()
                 {
                   Id = 25,
                   Name ="Соломоновы Острова",
                },
                        new MintCountry()
                 {
                   Id = 26,
                   Name ="Сомали",
                },
                new MintCountry()
                {
                   Id = 27,
                   Name ="США",
                },
                  new MintCountry()
                 {
                   Id = 28,
                   Name ="СССР",
                },
                  new MintCountry()
                 {
                   Id = 29,
                   Name ="Тувалу",
                },
                    new MintCountry()
                 {
                   Id = 30,
                   Name ="Украина",
                },
                      new MintCountry()
                 {
                   Id = 31,
                   Name ="Фиджи",
                },
                 new MintCountry()
                {
                   Id = 32,
                   Name ="Франция",
                },
                new MintCountry()
                 {
                   Id = 33,
                   Name ="Швейцария",
                },
                 new MintCountry()
                 {
                   Id = 34,
                   Name ="ЮАР",
                },
                   new MintCountry()
                 {
                   Id = 35,
                   Name ="Южная Корея",
                },
             };

            // data.ToList().ForEach(x => x.TranslitName = Transliteration.Generate(x.Name));

            builder.Entity<MintCountry>().HasData(data);
        }
    }
}