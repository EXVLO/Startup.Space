using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.Property(u => u.Id).HasColumnName("user_id");

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Surname).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired();

        builder.HasIndex(u => u.NormalizedEmail).IsUnique().HasDatabaseName("ix_user_normalized_email");
        builder.HasIndex(u => u.NormalizedUserName).IsUnique().HasDatabaseName("ix_user_normalized_user_name");
    }
}
