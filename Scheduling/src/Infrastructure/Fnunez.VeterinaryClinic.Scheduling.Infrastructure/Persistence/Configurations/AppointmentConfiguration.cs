using Fnunez.VeterinaryClinic.Scheduling.Domain.AppointmentAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever();
        
        builder.Property(a => a.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(a => a.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.OwnsOne(a => a.DateRange, a =>
        {
            a.Property(d => d.StartOn)
                .HasColumnName("DateRange_StartOn")
                .IsRequired();

            a.Property(d => d.EndOn)
                .HasColumnName("DateRange_EndOn")
                .IsRequired();
        });

        builder.HasOne(a => a.AppointmentType)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Client)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(a => a.Clinic)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Doctor)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Patient)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Room)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
