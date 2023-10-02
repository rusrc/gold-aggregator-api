using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace GoldAggregator.Parser.Entities.Entities
{
    /// <summary>
    /// История о предложении о продаже монеты
    /// </summary>
    public class CoinPriceHistory
    {
        public int Id { get; set; }
        /// <summary>
        /// Наименование позиции у дилера
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на конкретное предложение дилера
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Цена покупки
        /// </summary>
        public decimal? PriceToBuy { get; set; }

        /// <summary>
        /// Цена покупки  цена за грамм
        /// </summary>
        public decimal? PriceToBuyPerGram { get; set; }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public decimal? PriceToSell { get; set; }

        /// <summary>
        /// Цена продажи цена за грамм
        /// </summary>
        public decimal? PriceToSellPerGram { get; set; }

        /// <summary>
        /// Допольнительная цена со всякой особой скидкой, клупной скидкой и другой ерундой
        /// </summary>
        public decimal? PriceSpecial { get; set; }

        /// <summary>
        /// Цена продажи цена за грамм
        /// </summary>
        public decimal? PriceSpecialPerGram { get; set; }

        /// <summary>
        /// Информация об условиях специальной цены
        /// </summary>
        public string PriceSpecialDetails { get; set; }
        public DateTime ParseDate { get; set; }

        public int DealerId { get; set; }
        /// <summary>
        /// Дилер предлагающий в продаже монету
        /// </summary>
        public virtual Dealer Dealer { get; set; }
        /// <summary>
        /// TODO ?
        /// </summary>
        public int? CoinFromCatalogId { get; set; }
        /// <summary>
        /// Монета в нашем каталоге
        /// </summary>
        public virtual CoinCatalog CoinCatalog { get; set; }

        /// <summary>
        /// Количество кликов и переходов по ссылке с ценой на монету
        /// </summary>
        public virtual ICollection<CoinPriceClick> CoinsRedirects { get; set; }
    }

    public class CoinPriceHistoryConfiguration : IEntityTypeConfiguration<CoinPriceHistory>
    {
        public void Configure(EntityTypeBuilder<CoinPriceHistory> builder)
        {
            builder
                .HasOne(x => x.Dealer)
                .WithMany(x => x.CoinsPriceHistory)
                .HasForeignKey(x => x.DealerId);

            builder
               .HasOne(x => x.CoinCatalog)
               .WithMany(x => x.CoinsPriceHistory)
               .HasForeignKey(x => x.CoinFromCatalogId);                 
        }
    }
}
