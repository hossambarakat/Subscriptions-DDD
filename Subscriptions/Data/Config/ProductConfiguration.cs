using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriptions.Domain;

namespace Subscriptions.Data.Config
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
            builder.Property(x => x.Name);
            builder.Property(p => p.Amount)
                .HasColumnName("Amount")
                .HasColumnType("money");
            builder.Property(x => x.BillingPeriod)
                .HasColumnName("BillingPeriod")
                .HasConversion(period => period.Name,
                    name => BillingPeriod.FromName(name, true));
            builder.HasMany(x => x.Tags)
                .WithMany(x => x.Products);
        }
    }
}