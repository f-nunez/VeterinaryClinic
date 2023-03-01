using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserRoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Configurations;

public class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(aur => aur.Id);

        builder.Property(aur => aur.Id)
            .ValueGeneratedNever();

        builder.Property(aur => aur.RoleId)
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(aur => aur.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.HasOne(aur => aur.Role)
            .WithMany()
            .HasForeignKey(aur => aur.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(aur => aur.User)
            .WithMany(au => au.UserRoles)
            .HasForeignKey(aur => aur.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}