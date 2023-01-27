namespace Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

public interface IEntity
{
    public IReadOnlyCollection<BaseDomainEvent> DomainEvents { get; }

    public void AddDomainEvent(BaseDomainEvent domainEvent);
    public void ClearDomainEvents();
    public void RemoveDomainEvent(BaseDomainEvent domainEvent);
}