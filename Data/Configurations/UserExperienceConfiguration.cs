using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class UserExperienceConfiguration : IEntityTypeConfiguration<UserExperience>
{
    public void Configure(EntityTypeBuilder<UserExperience> builder)
    {
        builder.ToTable("user_experience");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("user_exp_id");

        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(e => e.User)
            .WithMany(u => u.Experiences)
            .HasForeignKey(e => e.UserId);
    }
}
