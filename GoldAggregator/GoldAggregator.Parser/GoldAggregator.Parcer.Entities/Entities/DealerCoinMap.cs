using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace GoldAggregator.Parser.Entities.Entities
{
    /// <summary>
    /// Класс для сопоставления монет от дилеров с каталогом
    /// </summary>
    public class DealerCoinMap
    {
        public int Id { get; set; }
        /// <summary>
        /// Уникальное название монеты у дилера
        /// </summary>
        public string MatchedTitle { get; set; }
        /// <summary>
        /// Уникальный Url на монету у дилера
        /// </summary>
        public string Url { get; set; }

        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }

        public int CoinFromCatalogId { get; set; }
        public virtual CoinCatalog CoinCatalog { get; set; }
    }


    public class DealerCoinMapConfiguration : IEntityTypeConfiguration<DealerCoinMap>
    {
        public void Configure(EntityTypeBuilder<DealerCoinMap> builder)
        {
            builder
                .HasOne(x => x.Dealer)
                .WithMany(x => x.DillerCoinMaps)
                .HasForeignKey(x => x.DealerId);

            builder
               .HasOne(x => x.CoinCatalog)
               .WithMany(x => x.DillerCoinMaps)
               .HasForeignKey(x => x.CoinFromCatalogId);
        }
    }
}
