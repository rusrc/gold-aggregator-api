using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Web.Mvc;

namespace GoldAggregator.Parser.Entities.Entities
{
    public class DealerOffice
    {
        public int Id { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        // TODO replace AllowHtml into DTO model later
        [AllowHtml] public string WorkTime { get; set; }
        public string Info { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
    }

    public class DealerOfficeConfiguration : IEntityTypeConfiguration<DealerOffice>
    {
        public void Configure(EntityTypeBuilder<DealerOffice> builder)
        {
            builder.HasOne(u => u.Dealer)
                .WithMany(ui => ui.Offices)
                .HasForeignKey(ui => ui.DealerId);

            builder.HasOne(u => u.City)
                .WithMany(ui => ui.Offices)
                .HasForeignKey(ui => ui.CityId);
        }
    }
}
