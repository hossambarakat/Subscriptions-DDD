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
                .HasColumnName("SubscriptionID");
            builder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<SubscriptionStatus>());
            builder.Property(x=>x.Amount)
                .HasColumnType("money");
            builder.HasOne(x => x.Product);
            builder.HasOne(x => x.Customer);
        }
    }
}