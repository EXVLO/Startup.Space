using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class NewsTagConfiguration : IEntityTypeConfiguration<NewsTag>
{
    public void Configure(EntityTypeBuilder<NewsTag> builder)
    {
        builder.ToTable("news_tag");

        builder.HasKey(nt => new { nt.NewsId, nt.TagId });

        builder.Property(nt => nt.NewsId).HasColumnName("news_id");
        builder.Property(nt => nt.TagId).HasColumnName("tag_id");

        builder.HasOne(nt => nt.News)
            .WithMany(n => n.NewsTags)
            .HasForeignKey(nt => nt.NewsId);

        builder.HasOne(nt => nt.Tag)
            .WithMany(t => t.NewsTags)
            .HasForeignKey(nt => nt.TagId);
    }
}
