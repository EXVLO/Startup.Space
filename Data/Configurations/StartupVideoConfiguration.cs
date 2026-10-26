using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupVideoConfiguration : IEntityTypeConfiguration<StartupVideo>
{
    public void Configure(EntityTypeBuilder<StartupVideo> builder)
    {
        builder.ToTable("startup_video");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("startup_video_id");

        builder.Property(v => v.StartupId).HasColumnName("startup_id");
        builder.Property(v => v.AddressKey).IsRequired();

        builder.HasOne(v => v.Startup)
            .WithMany(s => s.Videos)
            .HasForeignKey(v => v.StartupId);
    }
}
