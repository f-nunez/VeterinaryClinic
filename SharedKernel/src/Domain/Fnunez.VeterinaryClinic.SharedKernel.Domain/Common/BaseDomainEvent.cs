using MediatR;

namespace Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

public abstract class BaseDomainEvent : INotification
{
    public DateTimeOffset OccurredOn { get; protected set; } = DateTimeOffset.UtcNow;
}