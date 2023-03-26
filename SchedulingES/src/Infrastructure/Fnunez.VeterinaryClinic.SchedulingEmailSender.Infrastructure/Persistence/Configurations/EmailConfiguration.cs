using Fnunez.VeterinaryClinic.SchedulingEmailSender.Domain.EmailAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Configurations;

public class EmailConfiguration : IEntityTypeConfiguration<Email>
{
    public void Configure(EntityTypeBuilder<Email> builder)
    {
        builder.Property(e => e.Address)
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(e => e.Payload)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(e => e.TriggeredByUserId)
            .HasMaxLength(450)
            .IsRequired();
    }
}