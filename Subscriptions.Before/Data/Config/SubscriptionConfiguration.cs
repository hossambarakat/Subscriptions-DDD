using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subscriptions.Before.Domain;

namespace Subscriptions.Before.Data.Config
{
    public class SubscriptionConfiguration: IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscription");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(c => c.Value, guid => new SubscriptionId(guid))
                .HasColumnName("SubscriptionID")
                .ValueGeneratedNever();
            builder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<SubscriptionStatus>());
            builder.Property(x=>x.Amount)
                .HasColumnType("money");
            builder.Property(x => x.CurrentPeriodEndDate)
                .HasColumnType("date");
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x=>x.ProductId);
            builder.Property(x => x.ProductId)
                .HasConversion(c => c.Value, guid => new ProductId(guid));
            
            builder.Property(x => x.CustomerId)
                .HasConversion(c => c.Value, guid => new CustomerId(guid));
            builder.HasOne<Customer>()
                .WithMany(x=>x.Subscriptions)
                .HasForeignKey(x=>x.CustomerId);
        }
    }
}