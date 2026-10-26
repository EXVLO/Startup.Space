using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupCategoryConfiguration : IEntityTypeConfiguration<StartupCategory>
{
    public void Configure(EntityTypeBuilder<StartupCategory> builder)
    {
        builder.ToTable("startup_category");

        builder.HasKey(sc => new { sc.StartupId, sc.CategoryId });

        builder.Property(sc => sc.StartupId).HasColumnName("startup_id");
        builder.Property(sc => sc.CategoryId).HasColumnName("category_id");

        builder.HasOne(sc => sc.Startup)
            .WithMany(s => s.StartupCategories)
            .HasForeignKey(sc => sc.StartupId);

        builder.HasOne(sc => sc.Category)
            .WithMany(c => c.StartupCategories)
            .HasForeignKey(sc => sc.CategoryId);
    }
}
