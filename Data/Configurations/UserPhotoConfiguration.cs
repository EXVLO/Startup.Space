using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class UserPhotoConfiguration : IEntityTypeConfiguration<UserPhoto>
{
    public void Configure(EntityTypeBuilder<UserPhoto> builder)
    {
        builder.ToTable("user_photo");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("user_photo_id");

        builder.Property(p => p.UserId).HasColumnName("user_id");
        builder.Property(p => p.AddressKey).IsRequired();

        builder.HasOne(p => p.User)
            .WithOne(u => u.Photo)
            .HasForeignKey<UserPhoto>(p => p.UserId);
    }
}
