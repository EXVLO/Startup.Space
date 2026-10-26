using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupDocumentConfiguration : IEntityTypeConfiguration<StartupDocument>
{
    public void Configure(EntityTypeBuilder<StartupDocument> builder)
    {
        builder.ToTable("startup_document");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("startup_document_id");

        builder.Property(d => d.StartupId).HasColumnName("startup_id");
        builder.Property(d => d.AddressKey).IsRequired();

        builder.HasOne(d => d.Startup)
            .WithMany(s => s.Documents)
            .HasForeignKey(d => d.StartupId);
    }
}
