using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace GoldAggregator.Parser.Entities.Entities
{
    public class City
    {
        /// <summary>
        /// Id города, если null - Вся Россия без привязки к городу
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Название города
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Транслитирация названия
        /// </summary>
        public string TranslitName { get; set; }

        /// <summary>
        /// Связь города с офисами расположенными в нем
        /// </summary>
        public virtual ICollection<DealerOffice> Offices { get; set; }

    }

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
          
        }
    }
}
