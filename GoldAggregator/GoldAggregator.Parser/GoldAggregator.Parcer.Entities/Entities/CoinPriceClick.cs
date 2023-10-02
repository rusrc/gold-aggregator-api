using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldAggregator.Parser.Entities.Entities
{
    public class CoinPriceClick
    {
        public int Id { get; set; }
        /// <summary>
        /// Ip адрес
        /// </summary>
        public string Ip { get; set; }

        public  DateTime  RedirectDate { get; set; }

        public int CoinHistoryId { get; set; }

        public int? CityId { get; set; }

        public bool? IsClickBySellPrice { get; set; }

        public virtual CoinPriceHistory CoinHistory { get; set; }
    }

    public class CoinPriceClickConfiguration : IEntityTypeConfiguration<CoinPriceClick>
    {
        public void Configure(EntityTypeBuilder<CoinPriceClick> builder)
        {
            builder
                .HasOne(x => x.CoinHistory)
                .WithMany(x => x.CoinsRedirects)
                .HasForeignKey(x => x.CoinHistoryId);         
        }
    }
}
