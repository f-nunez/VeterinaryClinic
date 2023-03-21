using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(au => au.Id);

        builder.Property(au => au.Id)
            .ValueGeneratedNever()
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(au => au.Name)
            .HasMaxLength(450)
            .IsRequired();
    }
}