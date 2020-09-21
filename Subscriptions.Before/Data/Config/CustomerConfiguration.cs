using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriptions.Before.Domain;

namespace Subscriptions.Before.Data.Config
{
    public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("CustomerID")
                .ValueGeneratedNever();
            builder.Property(p => p.FirstName).HasColumnName("FirstName");
            builder.Property(p => p.LastName).HasColumnName("LastName");
            builder.Property(p => p.Email).HasColumnName("Email");
            builder.Property(p => p.MoneySpent)
                .HasColumnType("money");
        }
    }
}