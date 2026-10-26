using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StartupLab.Models.Entities;

namespace StartupLab.Data.Configurations;

public class UserContactConfiguration : IEntityTypeConfiguration<UserContact>
{
    public void Configure(EntityTypeBuilder<UserContact> builder)
    {
        builder.ToTable("user_contact");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("user_contact_id");

        builder.Property(c => c.UserId).HasColumnName("user_id");
        builder.Property(c => c.ContactInfo).IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.UserId);
    }
}
