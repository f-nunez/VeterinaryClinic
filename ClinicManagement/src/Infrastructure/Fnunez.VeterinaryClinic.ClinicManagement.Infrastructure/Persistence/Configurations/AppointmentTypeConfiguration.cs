using Fnunez.VeterinaryClinic.ClinicManagement.Domain.AppointmentTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Configurations;

public class AppointmentTypeConfiguration : IEntityTypeConfiguration<AppointmentType>
{
    public void Configure(EntityTypeBuilder<AppointmentType> builder)
    {
        builder.Property(a => a.Code)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.CreatedBy)
            .HasMaxLength(450);

        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.UpdatedBy)
            .HasMaxLength(450);
    }
}