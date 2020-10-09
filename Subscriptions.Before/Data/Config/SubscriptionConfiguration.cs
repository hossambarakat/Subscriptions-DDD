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
                .HasColumnName("SubscriptionID")
                .ValueGeneratedNever();
            builder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<SubscriptionStatus>())
                .IsRequired();
            builder.Property(x=>x.Amount)
                .IsRequired()
                .HasColumnType("money");
            builder.Property(x => x.CurrentPeriodEndDate)
                .IsRequired()
                .HasColumnType("date");
            builder.HasOne(x => x.Product)
                .WithMany()
                .IsRequired();
            builder.HasOne(x => x.Customer)
                .WithMany(x=>x.Subscriptions)
                .IsRequired();
        }
    }
}