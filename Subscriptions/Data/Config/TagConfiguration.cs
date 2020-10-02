using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Subscriptions.Domain;

namespace Subscriptions.Data.Config
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("TagID")
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}