namespace Contracts;

public abstract class BaseIntegrationEventContract
{
    public Guid CausationId { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn = DateTimeOffset.UtcNow;
}