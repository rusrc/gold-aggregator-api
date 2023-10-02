
using GoldAggregator.Parser.Entities.Entities;
//using GoldAggregator.Parser.Services;

using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class CitiesSeeding
    {
        public static void SeedCities(this ModelBuilder builder)
        {
            var data = new[]
            {
                new City()
                {
                   Id = 1,
                   Name ="Москва",
                   //TranslitName = Transliteration.Generate("Москва")
                },
                new City()
                {
                   Id = 2,
                   Name ="Санкт-Петербург",
                   //TranslitName = Transliteration.Generate("Санкт-Петербург")
                },
                new City()
                {
                   Id = 3,
                   Name ="Нижний Новгород",
                   //TranslitName = Transliteration.Generate("Нижний Новгород")
                },
                new City()
                {
                   Id = 4,
                   Name ="Казань",
                   //TranslitName = Transliteration.Generate("Казань")
                },
                new City()
                {
                   Id = 5,
                   Name ="Сургут",
                   //TranslitName = Transliteration.Generate("Сургут")
                },
                new City()
                {
                   Id = 6,
                   Name ="Севастополь",
                   //TranslitName = Transliteration.Generate("Севастополь")
                },
             };
            builder.Entity<City>().HasData(data);
        }
    }
}