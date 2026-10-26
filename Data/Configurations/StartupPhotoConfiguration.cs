using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupPhotoConfiguration : IEntityTypeConfiguration<StartupPhoto>
{
    public void Configure(EntityTypeBuilder<StartupPhoto> builder)
    {
        builder.ToTable("startup_photo");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("startup_photo_id");

        builder.Property(p => p.StartupId).HasColumnName("startup_id");
        builder.Property(p => p.AddressKey).IsRequired();

        builder.HasOne(p => p.Startup)
            .WithMany(s => s.Photos)
            .HasForeignKey(p => p.StartupId);
    }
}
