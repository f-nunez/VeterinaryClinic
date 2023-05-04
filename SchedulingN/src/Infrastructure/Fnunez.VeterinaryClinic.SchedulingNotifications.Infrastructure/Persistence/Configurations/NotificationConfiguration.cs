using Fnunez.VeterinaryClinic.SchedulingNotifications.Domain.NotificationAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.SchedulingNotifications.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.Property(n => n.Payload)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(n => n.TriggeredByUserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.HasOne(n => n.TriggeredByUser)
            .WithMany()
            .OnDelete(DeleteBehavior.NoAction);
    }
}