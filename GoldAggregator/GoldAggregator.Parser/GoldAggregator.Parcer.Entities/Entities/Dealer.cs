using GoldAggregator.Parcer.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldAggregator.Parser.Entities.Entities
{
    public class Dealer : SeoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProviderName { get; set; }
        public string BaseUrl { get; set; }

        /// <summary>
        /// Есть ли доставка по всей РФ?
        /// </summary>
        public bool? HasDelivery { get; set; }
        public DealerTypeEnum DealerType { get; set; }

        public virtual ICollection<DealerOffice> Offices { get; set; }

        /// <summary>
        /// Список url для парсинга по каждому дилеру
        /// </summary>
        public virtual ICollection<Url> Urls { get; set; }
        public virtual ICollection<CoinPrice> CoinPrices { get; set; }
        public virtual ICollection<CoinPriceHistory> CoinsPriceHistory { get; set; }

        /// <summary>
        /// Связь дилера со всеми монетами от дилера с их уникальными наименованиями
        /// </summary>
        public virtual ICollection<DealerCoinMap> DillerCoinMaps { get; set; }
    }

    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.HasKey(x => x.Id);           
        }
    }
}
