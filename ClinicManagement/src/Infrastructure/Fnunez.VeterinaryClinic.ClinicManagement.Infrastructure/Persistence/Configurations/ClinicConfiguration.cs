using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClinicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Persistence.Configurations;

public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
{
    public void Configure(EntityTypeBuilder<Clinic> builder)
    {
        builder.Property(c => c.Address)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(c => c.EmailAddress)
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}