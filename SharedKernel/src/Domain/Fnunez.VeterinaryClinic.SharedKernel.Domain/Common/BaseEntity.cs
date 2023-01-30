using System.ComponentModel.DataAnnotations.Schema;

namespace Fnunez.VeterinaryClinic.SharedKernel.Domain.Common;

public abstract class BaseEntity<TId> : IEntity
{
    public TId? Id { get; set; }

    private readonly List<BaseDomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void RemoveDomainEvent(BaseDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
}