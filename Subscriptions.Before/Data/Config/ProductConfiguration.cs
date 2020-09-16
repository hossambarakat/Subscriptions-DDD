using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subscriptions.Before.Domain;

namespace Subscriptions.Before.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ProductID");
            builder.Property(x => x.Name);
            builder.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("money");
            builder.Property(p => p.BillingPeriod)
                .HasColumnName("BillingPeriod")
                .HasConversion(new EnumToStringConverter<BillingPeriod>());

        }
    }
}