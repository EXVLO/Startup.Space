using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class ToolkitChecklistProgressConfiguration : IEntityTypeConfiguration<ToolkitChecklistProgress>
{
    public void Configure(EntityTypeBuilder<ToolkitChecklistProgress> builder)
    {
        builder.ToTable("toolkit_checklist_progress");

        builder.HasKey(tp => new { tp.UserId, tp.ToolkitChecklistId });

        builder.Property(tp => tp.UserId).HasColumnName("user_id");
        builder.Property(tp => tp.ToolkitChecklistId).HasColumnName("toolkit_checklist_id");

        builder.HasOne(tp => tp.User)
            .WithMany(u => u.ToolkitChecklistProgress)
            .HasForeignKey(tp => tp.UserId);

        builder.HasOne(tp => tp.ToolkitChecklist)
            .WithMany(c => c.ToolkitChecklistProgress)
            .HasForeignKey(tp => tp.ToolkitChecklistId);
    }
}
