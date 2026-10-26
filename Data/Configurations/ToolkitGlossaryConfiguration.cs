using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class ToolkitGlossaryConfiguration : IEntityTypeConfiguration<ToolkitGlossary>
{
    public void Configure(EntityTypeBuilder<ToolkitGlossary> builder)
    {
        builder.ToTable("toolkit_glossary");

        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).HasColumnName("toolkit_glossary_id");

        builder.Property(g => g.AddressKey).IsRequired();
        builder.Property(g => g.Term).IsRequired();
        builder.Property(g => g.Definition).IsRequired();
    }
}
