using Fnunez.VeterinaryClinic.Scheduling.Domain.ScheduleAggregate.Entities;
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

        builder.OwnsOne(a => a.DateRange, a =>
        {
            a.Property(d => d.StartOn)
                .HasColumnName("DateRange_StartOn")
                .IsRequired();

            a.Property(d => d.EndOn)
                .HasColumnName("DateRange_EndOn")
                .IsRequired();
        });

        builder.Property(a => a.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(a => a.AppointmentType)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Client)
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
