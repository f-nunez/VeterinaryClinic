namespace Contracts.ClinicManagement;

public class IntegrationEventClinicManagementContract
{
    public Guid CausationId { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn = DateTimeOffset.UtcNow;
    public string? IntegrationEvent { get; set; }
    public string? SerializedIntegrationEvent { get; set; }
}