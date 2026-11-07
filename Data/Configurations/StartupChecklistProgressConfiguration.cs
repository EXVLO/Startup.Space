using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupChecklistProgressConfiguration : IEntityTypeConfiguration<StartupChecklistProgress>
{
    public void Configure(EntityTypeBuilder<StartupChecklistProgress> builder)
    {
        builder.ToTable("startup_checklist_progress");

        builder.HasKey(sp => new { sp.StartupId, sp.StartupChecklistId });

        builder.Property(sp => sp.StartupId).HasColumnName("startup_id");
        builder.Property(sp => sp.StartupChecklistId).HasColumnName("startup_checklist_id");

        builder.HasOne(sp => sp.Startup)
            .WithMany(s => s.ChecklistProgress)
            .HasForeignKey(sp => sp.StartupId);

        builder.HasOne(sp => sp.StartupChecklist)
            .WithMany(c => c.Progress)
            .HasForeignKey(sp => sp.StartupChecklistId);
    }
}
