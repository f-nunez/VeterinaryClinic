namespace Contracts.ClinicManagement;

public class NotificationRequestClinicManagementContract
{
    public Guid CausationId { get; set; } = Guid.NewGuid();
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset OccurredOn = DateTimeOffset.UtcNow;
    public string? NotificationEvent { get; set; }
    public string? SerializedNotificationRequest { get; set; }
}