using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class ToolkitDocumentConfiguration : IEntityTypeConfiguration<ToolkitDocument>
{
    public void Configure(EntityTypeBuilder<ToolkitDocument> builder)
    {
        builder.ToTable("toolkit_document");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("toolkit_document_id");

        builder.Property(d => d.AddressKey).IsRequired();
    }
}
