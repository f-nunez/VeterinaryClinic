using Fnunez.VeterinaryClinic.ClinicManagement.Domain.ClientAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(p => p.AnimalType, p =>
        {
            p.Property(at => at.Breed)
                .HasColumnName("AnimalType_Breed")
                .HasMaxLength(200)
                .IsRequired();

            p.Property(at => at.Species)
                .HasColumnName("AnimalType_Species")
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.OwnsOne(p => p.Photo, p =>
        {
            p.Property(p => p.Name)
                .HasColumnName("Photo_Name")
                .HasMaxLength(200)
                .IsRequired();

            p.Property(p => p.StoredName)
                .HasColumnName("Photo_StoredName")
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.Metadata
            .FindNavigation(nameof(Patient.AnimalType))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Patient.Photo))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}