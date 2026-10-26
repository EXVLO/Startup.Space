using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupLinkConfiguration : IEntityTypeConfiguration<StartupLink>
{
    public void Configure(EntityTypeBuilder<StartupLink> builder)
    {
        builder.ToTable("startup_link");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("startup_link_id");

        builder.Property(l => l.StartupId).HasColumnName("startup_id");
        builder.Property(l => l.Link).IsRequired();

        builder.HasOne(l => l.Startup)
            .WithMany(s => s.Links)
            .HasForeignKey(l => l.StartupId);
    }
}
