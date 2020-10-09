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
            builder.Property(x => x.Id)
                .HasColumnName("ProductID")
                .ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnName("Amount")
                .HasColumnType("money");
            builder.Property(p => p.BillingPeriod)
                .IsRequired()
                .HasColumnName("BillingPeriod")
                .HasConversion(new EnumToStringConverter<BillingPeriod>());

        }
    }
}