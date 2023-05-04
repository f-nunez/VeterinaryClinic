using Fnunez.VeterinaryClinic.ClinicManagementNotifications.Domain.AppNotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.ClinicManagementNotifications.Infrastructure.Persistence.Configurations;

public class AppNotificationConfiguration : IEntityTypeConfiguration<AppNotification>
{
    public void Configure(EntityTypeBuilder<AppNotification> builder)
    {
        builder.HasKey(an => an.Id);

        builder.Property(an => an.Id)
            .ValueGeneratedNever();

        builder.Property(an => an.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.HasOne(an => an.Notification)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}