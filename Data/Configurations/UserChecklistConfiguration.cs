using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class UserChecklistConfiguration : IEntityTypeConfiguration<UserChecklist>
{
    public void Configure(EntityTypeBuilder<UserChecklist> builder)
    {
        builder.ToTable("user_checklist");

        builder.HasKey(uc => new { uc.UserId, uc.ToolkitChecklistId });

        builder.Property(uc => uc.UserId).HasColumnName("user_id");
        builder.Property(uc => uc.ToolkitChecklistId).HasColumnName("toolkit_checklist_id");

        builder.HasOne(uc => uc.User)
            .WithMany(u => u.UserChecklists)
            .HasForeignKey(uc => uc.UserId);

        builder.HasOne(uc => uc.ToolkitChecklist)
            .WithMany(c => c.UserChecklists)
            .HasForeignKey(uc => uc.ToolkitChecklistId);
    }
}
