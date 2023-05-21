namespace Contracts.Public;

public class AppointmentConfirmedIntegrationEventContract
{
    public Guid CausationId { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn = DateTimeOffset.UtcNow;
    public Guid AppointmentId { get; set; } = Guid.Empty;
}