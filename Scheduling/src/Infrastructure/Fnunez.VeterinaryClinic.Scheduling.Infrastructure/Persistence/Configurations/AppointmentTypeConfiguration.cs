using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.AppointmentTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Configurations;

public class AppointmentTypeConfiguration : IEntityTypeConfiguration<AppointmentType>
{
    public void Configure(EntityTypeBuilder<AppointmentType> builder)
    {
        builder.Property(a => a.Code)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}