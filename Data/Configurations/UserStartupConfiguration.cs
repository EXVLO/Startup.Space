using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class UserStartupConfiguration : IEntityTypeConfiguration<UserStartup>
{
    public void Configure(EntityTypeBuilder<UserStartup> builder)
    {
        builder.ToTable("user_startup");

        builder.HasKey(us => new { us.UserId, us.StartupId });

        builder.Property(us => us.UserId).HasColumnName("user_id");
        builder.Property(us => us.StartupId).HasColumnName("startup_id");

        builder.HasOne(us => us.User)
            .WithMany(u => u.UserStartups)
            .HasForeignKey(us => us.UserId);

        builder.HasOne(us => us.Startup)
            .WithMany(s => s.UserStartups)
            .HasForeignKey(us => us.StartupId);
    }
}
