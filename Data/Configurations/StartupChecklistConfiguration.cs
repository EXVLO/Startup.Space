using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupChecklistConfiguration : IEntityTypeConfiguration<StartupChecklist>
{
    public void Configure(EntityTypeBuilder<StartupChecklist> builder)
    {
        builder.ToTable("startup_checklist");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("startup_checklist_id");

        builder.Property(c => c.Label).IsRequired();
    }
}
