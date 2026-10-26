using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class ToolkitVideoConfiguration : IEntityTypeConfiguration<ToolkitVideo>
{
    public void Configure(EntityTypeBuilder<ToolkitVideo> builder)
    {
        builder.ToTable("toolkit_video");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasColumnName("toolkit_video_id");

        builder.Property(v => v.AddressKey).IsRequired();
    }
}
