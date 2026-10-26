using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class StartupConfiguration : IEntityTypeConfiguration<Startup>
{
    public void Configure(EntityTypeBuilder<Startup> builder)
    {
        builder.ToTable("startup");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("startup_id");

        builder.Property(s => s.Name).IsRequired();
    }
}
