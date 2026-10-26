using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class NewsPhotoConfiguration : IEntityTypeConfiguration<NewsPhoto>
{
    public void Configure(EntityTypeBuilder<NewsPhoto> builder)
    {
        builder.ToTable("news_photo");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("news_photo_id");

        builder.Property(p => p.NewsId).HasColumnName("news_id");
        builder.Property(p => p.AddressKey).IsRequired();

        builder.HasOne(p => p.News)
            .WithMany(n => n.Photos)
            .HasForeignKey(p => p.NewsId);
    }
}
