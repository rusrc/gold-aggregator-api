using System;
using GoldAggregator.Parser.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoldAggregator.Parser.Entities.Entities
{
    public class Url
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public int ExternalId { get; set; }
        public Status? Status { get; set; }

        public int DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
    }

    public class UrlConfiguration : IEntityTypeConfiguration<Url>
    {
        public void Configure(EntityTypeBuilder<Url> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(e => e.Value).IsUnique();

            builder.HasOne(u => u.Dealer)
                .WithMany(ui => ui.Urls)
                .HasForeignKey(ui => ui.DealerId);
        }
    }
}
