using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldAggregator.Parser.Entities.Entities
{
    public class MintCountry
    {
        /// <summary>
        /// Id страны
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        public string TranslitName { get; set; }

        /// <summary>
        /// Связь страны и монет/слитков выпуска данной страны
        /// </summary>
        public virtual ICollection<CoinCatalog> CatalogItems { get; set; }
    }

    public class CountryConfiguration : IEntityTypeConfiguration<MintCountry>
    {
        public void Configure(EntityTypeBuilder<MintCountry> builder)
        {
          
        }
    }
}
