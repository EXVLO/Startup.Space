using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class NewsVideoConfiguration : IEntityTypeConfiguration<NewsVideo>
{
    public void Configure(EntityTypeBuilder<NewsVideo> builder)
    {
        builder.ToTable("news_video");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("news_video_id");

        builder.Property(v => v.NewsId).HasColumnName("news_id");
        builder.Property(v => v.AddressKey).IsRequired();

        builder.HasOne(v => v.News)
            .WithMany(n => n.Videos)
            .HasForeignKey(v => v.NewsId);
    }
}
