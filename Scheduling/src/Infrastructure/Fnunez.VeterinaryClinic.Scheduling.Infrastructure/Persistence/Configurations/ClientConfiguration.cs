using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.ClientAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.Property(c => c.EmailAddress)
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(c => c.FullName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.PreferredName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Salutation)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(c => c.Patients)
            .WithOne()
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}