using GoldAggregator.Parser.Entities.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

namespace GoldAggregator.Parser.DbContext.Seedings
{
    public static class UrlSeeding
    {
        public static void SeedUrls(this ModelBuilder builder)
        {
            var data = new List<Url>
            {
                new Url() { Id = 1, DealerId = 1, Value = "https://zoloto-md.ru" },
                new Url() { Id = 2, DealerId = 2, Value = "https://monetainvest.ru" },
                new Url() { Id = 3, DealerId = 3, Value = "http://zoloto-piter.ru" }
            };

            builder.Entity<Url>().HasData(data);
        }
    }
}
