using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.ApplicationRoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.HasKey(ar => ar.Id);

        builder.Property(ar => ar.Id)
            .ValueGeneratedNever()
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(ar => ar.Name)
            .HasMaxLength(256)
            .IsRequired();
    }
}