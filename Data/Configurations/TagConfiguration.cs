using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tag");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("tag_id");

        builder.Property(t => t.Title).IsRequired();
    }
}
