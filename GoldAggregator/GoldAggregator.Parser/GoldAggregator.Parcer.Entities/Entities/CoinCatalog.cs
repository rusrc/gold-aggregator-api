using GoldAggregator.Parcer.Entities.Entities;
using GoldAggregator.Parcer.Entities.Extensions;
using GoldAggregator.Parser.Entities.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

using System.ComponentModel.DataAnnotations.Schema;

namespace GoldAggregator.Parser.Entities.Entities
{
    // TODO CoinCatalog
    /// <summary>
    /// Список всех монет в мире
    /// </summary>
    // [ModelBinder(BinderType = typeof(DecimalEntityBinder))]
    public class CoinCatalog : SeoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Транцлитирация на русском
        /// </summary>
        public string TranslitName { get; set; }
        /// <summary>
        /// Номинал
        /// </summary>
        public string Nomination { get; set; }
        /// <summary>
        /// Вес
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// Id страны монетного двора
        /// </summary>
        public int MintCountryId { get; set; }
        /// <summary>
        /// Золото / Серебро / Платина / Палладий
        /// </summary>
        public MetalTypeEnum MetalType { get; set; }
        /// <summary>
        /// Начальная дата выпуска. Если нет месяца и дня, пишем '01.01.1975'
        /// </summary>
        public DateTime? StartMiningYear { get; set; }
        /// <summary>
        /// Последняя дата выпуска. Если нет месяца или дня, пишем '31.12.2022'
        /// </summary>
        public DateTime? EndMiningYear { get; set; }
        /// <summary>
        /// Название изображения
        /// </summary>
        public string ImageName { get; set; }
        // TODO Remove later. Пока не решил, удалю позже
        public ICollection<Image> Images { get; set; }
        /// <summary>
        /// Страна монетного двора
        /// </summary>
        public virtual MintCountry MintCountry { get; set; }
        /// <summary>
        /// Связь каталога со всеми монетами от дилеров с их уникальными наименованиями
        /// </summary>
        public virtual ICollection<DealerCoinMap> DillerCoinMaps { get; set; }
        /// <summary>
        /// Связь каталога со всеми предложениями ее в продаже
        /// </summary>
        public virtual ICollection<CoinPrice> CoinPrices { get; set; }
        /// <summary>
        /// TODO description
        /// </summary>
        public virtual ICollection<CoinPriceHistory> CoinsPriceHistory { get; set; }

        #region No mapping
        [NotMapped]
        public string MetalTypeName { get => this.MetalType.GetMetalName(); }
        #endregion
    }

    public class CatalogConfiguration : IEntityTypeConfiguration<CoinCatalog>
    {
        public void Configure(EntityTypeBuilder<CoinCatalog> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.MintCountry)
                .WithMany(x => x.CatalogItems)
                .HasForeignKey(x => x.MintCountryId);
            
            // Read more https://www.npgsql.org/efcore/mapping/json.html?tabs=fluent-api%2Cpoco
            builder.Property(p => p.Images)
                .HasColumnType("jsonb");
        }
    }
}
