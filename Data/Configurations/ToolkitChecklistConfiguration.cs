using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class ToolkitChecklistConfiguration : IEntityTypeConfiguration<ToolkitChecklist>
{
    public void Configure(EntityTypeBuilder<ToolkitChecklist> builder)
    {
        builder.ToTable("toolkit_checklist");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("toolkit_checklist_id");

        builder.Property(c => c.Label).IsRequired();
    }
}
