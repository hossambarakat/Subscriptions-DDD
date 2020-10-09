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
            builder.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired();
            builder.Property(p => p.LastName).HasColumnName("LastName").IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired();
            builder.Property(p => p.MoneySpent)
                .HasColumnType("money");
        }
    }
}