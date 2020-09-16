using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriptions.Domain;

namespace Subscriptions.Data.Config
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("CustomerID");
            builder.OwnsOne(x => x.FullName, nameBuilder =>
            {
                nameBuilder.Property(p => p.FirstName).HasColumnName("FirstName");
                nameBuilder.Property(p => p.LastName).HasColumnName("LastName");
            });
            builder.OwnsOne(x => x.Email, nameBuilder =>
            {
                nameBuilder.Property(p => p.Value).HasColumnName("Email");
            });
            builder.HasMany(x => x.Subscriptions)
                .WithOne(x => x.Customer);
        }
    }
}