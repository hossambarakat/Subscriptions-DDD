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
            builder.Property(x => x.Id).HasColumnName("ProductID");
            builder.Property(x => x.Name);
            builder.OwnsOne(x => x.PricePlan, pricePlanBuilder =>
            {
                pricePlanBuilder.Property(p => p.Amount)
                    .HasColumnName("Amount")
                    .HasColumnType("money");
                pricePlanBuilder.Property(p => p.BillingPeriod)
                    .HasColumnName("BillingPeriod")
                    .HasConversion(period => period.Name,
                        name => BillingPeriod.FromName(name, true));
            });

        }
    }
}