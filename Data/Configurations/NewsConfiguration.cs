using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class NewsConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.ToTable("news");

        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).HasColumnName("news_id");

        builder.Property(n => n.Title).IsRequired();
    }
}
